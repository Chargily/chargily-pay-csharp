using System.Collections.Concurrent;
using System.Reactive.Linq;
using AutoMapper;
using Chargily.Pay.V2.Abstractions;
using Chargily.Pay.V2.Internal.Endpoints;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Polly;
using Polly.Retry;

namespace Chargily.Pay.V2.Internal;
/// <summary>
/// <b>Chargily Pay V2</b> client with builtin <i>Caching + Retry on failure</i> 
/// </summary>
internal class ResilientChargilyPayClient : IChargilyPayClient
{
  internal event Action OnDisposing;
  
  private readonly IChargilyPayApi _chargilyPayApi;
  private readonly IServiceProvider _provider;
  private readonly IOptions<ChargilyConfig> _config;
  private readonly IMemoryCache _cache;
  private readonly ResiliencePipeline _retryPipeline;
  private readonly ILogger<ResilientChargilyPayClient> _logger;
  private readonly IMapper _mapper;

  private IDisposable _balanceObservable;
  private IDisposable _cacheObservable;
  public void Dispose()
  {
    _balanceObservable?.Dispose();
    _cacheObservable?.Dispose();
    _cache?.Dispose();
    OnDisposing?.Invoke();
  }
  public ResilientChargilyPayClient(IMemoryCache cache,
                                   IMapper mapper,
                                   ILogger<ResilientChargilyPayClient> logger,
                                   IChargilyPayApi chargilyPayApi,
                                   IServiceProvider provider,
                                   IOptions<ChargilyConfig> config)
  {
    _mapper = mapper;
    _logger = logger;
    _cache = cache;
    _config = config;
    _chargilyPayApi = chargilyPayApi;
    _provider = provider;
    WebhookValidator = _provider.GetRequiredService<IWebhookValidator>();

    _retryPipeline = new ResiliencePipelineBuilder()
                    .AddRetry(new RetryStrategyOptions()
                              {
                                DelayGenerator =
                                  (attempt) => ValueTask.FromResult<TimeSpan?>(_config.Value.DelayPerRetryCalculator?.Invoke(attempt.AttemptNumber) ??
                                                                               TimeSpan.FromMilliseconds(500 * attempt.AttemptNumber)),
                                OnRetry = (ctx) =>
                                          {
                                            logger
                                            ?.LogInformation("Failed, Attempt number: {@number}/{@max}, retrying after {seconds:N3} seconds.",
                                                             ctx.AttemptNumber, _config.Value.MaxRetriesOnFailure, ctx.RetryDelay.TotalSeconds);
                                            logger?.LogError("Failed with Exception: {@ex}",
                                                             ctx.Outcome.Exception?.ToString());
                                            return ValueTask.CompletedTask;
                                          },
                                MaxRetryAttempts = _config.Value.MaxRetriesOnFailure,
                              })
                    .Build();
    StartDataRefreshers();
  }
  

  private readonly ConcurrentDictionary<EntityType, List<CancellationTokenSource>> _cancellations =
    new ConcurrentDictionary<EntityType, List<CancellationTokenSource>>();
  private event Action<EntityType> OnDataStale;
  private void StartDataRefreshers()
  {
    _cacheObservable = Observable.FromEvent<EntityType>(h => OnDataStale += h, h => OnDataStale -= h)
                                 .Buffer(TimeSpan.FromSeconds(5))
                                 .Select(list => list.Distinct())
                                 .Do(list =>
                                     {
                                       foreach (var type in list)
                                       {
                                         try
                                         {
                                           _cancellations[type].ForEach(x =>
                                                                        {
                                                                          try
                                                                          {
                                                                            x.Cancel();
                                                                            x.Dispose();
                                                                          }
                                                                          catch{}
                                                                        });
                                           _cancellations[type].Clear();
                                         }
                                         catch{}
                                         
                                       }
                                     })
                                 .Subscribe();
    _balanceObservable = Observable
                        .Interval(_config.Value.BalanceRefreshInterval)
                        .Do(async _ =>
                            {
                              try
                              {
                                await GetBalance();
                              }
                              catch {}
                            })
                        .Subscribe();

  }

  private IValidator<T> GetValidator<T>() => _provider.GetRequiredService<IValidator<T>>();

  private IChangeToken CreateCacheExpiration(EntityType entityType)
  {
    var cancellationTokenSource = new CancellationTokenSource();
    var cancellation = new CancellationChangeToken(cancellationTokenSource.Token);
    if(_cancellations.TryGetValue(entityType, out var list));
    {
      list?.Add(cancellationTokenSource);
    }
    return cancellation;
  }



  private async Task<List<T>> ExhaustAllPages<T>(Func<int, Task<PagedApiResponse<T>>> fetchFunction)
    where T : BaseObjectApiResponse
  {
    try
    {
      var current = await _retryPipeline.ExecuteAsync(async (_) => await fetchFunction(1));
      var result = new List<T>();
      do
      {
        result.AddRange(current.Data);
        if (current.GetNextPage() is not null)
          current = await _retryPipeline.ExecuteAsync(async (_) =>
                                                        await fetchFunction((int)current.GetNextPage()!));
      } while (current.GetNextPage() is not null && current.GetNextPage() != current.LastPage);

      return result;
    }
    catch (Exception e)
    {
      //_logger?.LogTrace("{@namFn} threw an exception: {@ex}", nameof(ExhaustAllPages), e.ToString());
      return [];
    }
  }

  public IWebhookValidator WebhookValidator { get; }
  public bool IsLiveMode => _config.Value.IsLiveMode;

#region Balance

  public IReadOnlyList<Wallet> Balance { get; private set; }
  public DateTimeOffset? BalanceRefreshedAt { get; private set; }

  public Task<IReadOnlyList<Wallet>?> GetBalance()
  {
    return _cache.GetOrCreateAsync<IReadOnlyList<Wallet>>(CacheKey.From(EntityType.Balance, _config.Value),
                                                          async (cacheEntry) =>
                                                          {
                                                            cacheEntry.AbsoluteExpirationRelativeToNow =
                                                              _config.Value.GetCacheDuration();
                                                            _logger?.LogInformation("Refreshing Account Balance...");
                                                            var response = await _retryPipeline.ExecuteAsync(async (_) =>
                                                                             await _chargilyPayApi.GetBalance());
                                                            var wallets = _mapper.Map<IReadOnlyList<Wallet>>(response);
                                                            _logger?.LogDebug("Fetched Balance:\n{@balance}", wallets.Stringify());
                                                            BalanceRefreshedAt = DateTimeOffset.Now;
                                                            Balance = wallets;
                                                            return wallets;
                                                          });
  }

#endregion

#region Product

  public async Task<Response<Product>> AddProduct(CreateProduct product)
  {
    var request = _mapper.Map<CreateProductRequest>(product);
    var validation = await GetValidator<CreateProductRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Creating a new Product... {@name}", product.Name);
    _logger?.LogDebug("Creating a new Product...\n{@request}", request.Stringify());
    var response = await _chargilyPayApi.CreateProduct(request);
    var result = _mapper.Map<Response<Product>>(response);
    _logger?.LogDebug("Product created:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.Product);
    return result;
  }

  public async Task<Response<Product>> UpdateProduct(UpdateCustomer update)
  {
    var request = _mapper.Map<UpdateProductRequest>(update);
    var validation = await GetValidator<UpdateProductRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Update Product of id: {@}...", request.Id);
    _logger?.LogDebug("Update Product of id: {@} with:\n{@request}", request.Id, request.Stringify(true));
    var response = await _chargilyPayApi.UpdateProduct(request.Id, request);
    var result = _mapper.Map<Response<Product>>(response);
    _logger?.LogDebug("Product updated:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.Product);
    return result;
  }

  public Task<Response<Product>?> GetProduct(string id)
  {
    return _cache.GetOrCreateAsync<Response<Product>?>(CacheKey.From(EntityType.Product, _config.Value, id),
                                                       async (cacheEntry) =>
                                                       {
                                                         cacheEntry.AbsoluteExpirationRelativeToNow =
                                                           _config.Value.GetCacheDuration();
                                                         _logger
                                                         ?.LogInformation("Fetching Product of id: {@id}...", id);
                                                         var response = await _retryPipeline.ExecuteAsync(async (_) =>
                                                                                                            await _chargilyPayApi.GetProduct(id));
                                                         var items = await GetProductItems(id);
                                                         var result = _mapper.Map<Response<Product>>((response, items));
                                                         _logger?.LogDebug("Fetched Product:\n{}",
                                                                           result.Stringify());

                                                         cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                         return result;
                                                       });
  }

  public Task<PagedResponse<Product>> GetProducts(int page = 1, int pageSize = 50)
  {
    return
      _cache.GetOrCreateAsync<PagedResponse<Product>>(CacheKey.From(EntityType.Product, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                                      async (cacheEntry) =>
                                                      {
                                                        cacheEntry.AbsoluteExpirationRelativeToNow =
                                                          _config.Value.GetCacheDuration();
                                                        _logger
                                                        ?.LogInformation("Fetching Products. Page: {@pageNumber}, Page Size: {@pageSize}",
                                                                         page, pageSize);
                                                        var response = await _retryPipeline.ExecuteAsync(async (_) =>
                                                                                                           await _chargilyPayApi.GetProducts(page, pageSize));
                                                        var products = new List<Product>();
                                                        foreach (var item in response.Data)
                                                        {
                                                          var items = await GetProductItems(item.Id);
                                                          var product = _mapper.Map<Product>((item, items));
                                                          products.Add(product);
                                                          _cache.Set(CacheKey.From(EntityType.Product, _config.Value, item.Id),
                                                                     product, _config.Value.GetCacheDuration());
                                                        }

                                                        var result = _mapper.Map<PagedResponse<Product>>((response, products));
                                                        _logger?.LogDebug("Fetched Products:\n{}", result.Stringify());
                                                        cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                        return result;
                                                      })!;
  }

  private Task<List<ProductPrice>?> GetProductItems(string productId)
  {
    return
      _cache.GetOrCreateAsync<List<ProductPrice>?>(CacheKey.From(EntityType.Product, _config.Value, productId, "items"),
                                                   async (cacheEntry) =>
                                                   {
                                                     cacheEntry.AbsoluteExpirationRelativeToNow =
                                                       _config.Value.GetCacheDuration();
                                                     _logger?.LogInformation("Fetching Prices of Product with id: {@id}...", productId);
                                                     var response = await ExhaustAllPages((p) => _chargilyPayApi.GetProductPrices(productId, p));
                                                     var result = _mapper.Map<List<ProductPrice>>(response);
                                                     _logger?.LogDebug("Fetched {@count} Prices.", result.Count);
                                                     cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                     cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Price));
                                                     return result;
                                                   })!;
  }

  public Task<List<Price>?> GetProductPrices(string productId)
  {
    return
      _cache.GetOrCreateAsync<List<Price>?>(CacheKey.From(EntityType.Product, _config.Value, productId, "prices"),
                                            async (cacheEntry) =>
                                            {
                                              cacheEntry.AbsoluteExpirationRelativeToNow =
                                                _config.Value.GetCacheDuration();
                                              _logger?.LogInformation("Fetching Prices of Product with id: {@id}...", productId);
                                              var response = await ExhaustAllPages((p) => _chargilyPayApi.GetProductPrices(productId, p));
                                              var result = new List<Price>();
                                              foreach (var item in result)
                                              {
                                                var product = await GetProduct(item.ProductId);
                                                var mapped = _mapper.Map<Price>((item, product));
                                                result.Add(mapped);
                                                _cache.Set(CacheKey.From(EntityType.Price, _config.Value, item.Id),
                                                           mapped, _config.Value.GetCacheDuration());
                                              }

                                              _logger?.LogDebug("Fetched {@count} Prices.", result.Count);
                                              return result;
                                            })!;
  }

  public async IAsyncEnumerable<Product> Products()
  {
    var current = await GetProducts(1);
    do
    {
      if (current.Data is not null)
        foreach (var item in current.Data)
          yield return _mapper.Map<Product>(item)!;
      if (current.NextPage is not null) current = await GetProducts((int)current.NextPage);
    } while (current.NextPage is not null && current.NextPage != current.LastPage);
  }

#endregion

#region Customer

  public async Task<Response<Customer>> AddCustomer(CreateCustomer customer)
  {
    var request = _mapper.Map<CreateCustomerRequest>(customer);
    var validation = await GetValidator<CreateCustomerRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Creating a new Customer... {@name}", customer.Name);
    _logger?.LogDebug("Creating a new Customer...\n{@request}", request.Stringify());
    var response = await _chargilyPayApi.CreateCustomer(request);
    var result = _mapper.Map<Response<Customer>>(response);
    _logger?.LogDebug("Customer created:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.Customer);
    return result;
  }

  public async Task<Response<Customer>> UpdateCustomer(UpdateCustomer update)
  {
    var request = _mapper.Map<UpdateCustomerRequest>(update);
    var validation = await GetValidator<UpdateCustomerRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Update Customer of id: {@}...", request.Id);
    _logger?.LogDebug("Update Customer of id: {@} with:\n{@request}", request.Id, request.Stringify(true));
    var response = await _chargilyPayApi.UpdateCustomer(request.Id, request);
    var result = _mapper.Map<Response<Customer>>(response);
    _logger?.LogDebug("Customer updated:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.Customer);
    return result;
  }

  public Task<PagedResponse<Customer>> GetCustomers(int page = 1, int pageSize = 50)
  {
    return
      _cache.GetOrCreateAsync<PagedResponse<Customer>>(CacheKey.From(EntityType.Customer, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                                       async (cacheEntry) =>
                                                       {
                                                         cacheEntry.AbsoluteExpirationRelativeToNow =
                                                           _config.Value.GetCacheDuration();
                                                         _logger
                                                         ?.LogInformation("Fetching Customers. Page: {@pageNumber}, Page Size: {@pageSize}",
                                                                          page, pageSize);
                                                         var response = await _retryPipeline.ExecuteAsync(async (_) =>
                                                                                                                await _chargilyPayApi
                                                                                                                 .GetCustomers(page, pageSize));
                                                         var result = _mapper.Map<PagedResponse<Customer>>(response);
                                                         foreach (var item in result.Data)
                                                         {
                                                           _cache.Set(CacheKey.From(EntityType.Customer, _config.Value, item.Id),
                                                                      item, _config.Value.GetCacheDuration());
                                                         }

                                                         _logger?.LogDebug("Fetched Customers:\n{}", result.Stringify());
                                                         cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Customer));
                                                         return result;
                                                       })!;
  }

  public Task<Response<Customer>?> GetCustomer(string id)
  {
    return _cache.GetOrCreateAsync<Response<Customer>?>(CacheKey.From(EntityType.Customer, _config.Value, id),
                                                        async (cacheEntry) =>
                                                        {
                                                          cacheEntry.AbsoluteExpirationRelativeToNow =
                                                            _config.Value.GetCacheDuration();
                                                          _logger
                                                          ?.LogInformation("Fetching Customer of id: {@id}...",
                                                                           id);
                                                          var response =
                                                            await _retryPipeline.ExecuteAsync(async (_) =>
                                                                                                await _chargilyPayApi.GetCustomer(id));
                                                          var result = _mapper.Map<Response<Customer>>(response);
                                                          _logger?.LogDebug("Fetched Customer:\n{}", result.Stringify());
                                                          cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Customer));
                                                          return result;
                                                        });
  }

  public async IAsyncEnumerable<Customer?> Customers()
  {
    var current = await GetCustomers(1);
    do
    {
      foreach (var item in current.Data)
        yield return item;
      if (current.NextPage is not null) current = await GetCustomers((int)current.NextPage);
    } while (current.NextPage is not null && current.NextPage != current.LastPage);
  }

#endregion

#region PaymentLink

  public async Task<Response<PaymentLinkResponse>> CreatePaymentLink(CreatePaymentLink paymentLink)
  {
    var request = _mapper.Map<CreatePaymentLinkRequest>(paymentLink);
    var validation = await GetValidator<CreatePaymentLinkRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Creating a new Payment-link...");
    _logger?.LogDebug("Creating a new Payment-link...\n{@request}", request.Stringify());
    var response = await _chargilyPayApi.CreatePaymentLink(request);
    var result = _mapper.Map<Response<PaymentLinkResponse>>(response);
    _logger?.LogInformation("Payment-link created, with id {@id} and url: {@url}",
                            result.Value.Id, result.Value.Url);
    _logger?.LogDebug("Payment-link created:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.PaymentLink);
    return result;
  }

  public async Task<Response<PaymentLinkResponse>> UpdatePaymentLink(UpdatePaymentLink update)
  {
    var request = _mapper.Map<UpdatePaymentLinkRequest>(update);
    var validation = await GetValidator<UpdatePaymentLinkRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Update Payment-link of id: {@}...", request.Id);
    _logger?.LogDebug("Update Payment-link of id: {@} with:\n{@request}", request.Id, request.Stringify(true));
    var response = await _chargilyPayApi.UpdatePaymentLink(request.Id, request);
    var result = _mapper.Map<Response<PaymentLinkResponse>>(response);
    _logger?.LogDebug("Payment-link updated:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.PaymentLink);
    return result;
  }

  public Task<PagedResponse<PaymentLinkResponse>> GetPaymentLinks(int page = 1, int pageSize = 50)
  {
    return
      _cache.GetOrCreateAsync<PagedResponse<PaymentLinkResponse>>(CacheKey.From(EntityType.PaymentLink, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                                                  async (cacheEntry) =>
                                                                  {
                                                                    cacheEntry.AbsoluteExpirationRelativeToNow = _config.Value.GetCacheDuration();
                                                                    _logger?.LogInformation("Fetching Payment Links. Page: {@pageNumber}, Page Size: {@pageSize}",
                                                                                            page, pageSize);
                                                                    var response = await _retryPipeline.ExecuteAsync(async (_) =>
                                                                                     await _chargilyPayApi.GetPaymentLinks(page, pageSize));
                                                                    var paymentLinkItems = new List<PaymentLinkResponse>();
                                                                    foreach (var item in response.Data)
                                                                    {
                                                                      var items = await GetPaymentLinkItems(item.Id);
                                                                      var paymentLink = _mapper.Map<PaymentLinkResponse>((item, items));
                                                                      paymentLinkItems.Add(paymentLink);
                                                                    }

                                                                    var result = _mapper.Map<PagedResponse<PaymentLinkResponse>>((response, paymentLinkItems));

                                                                    _logger?.LogDebug("Fetched Payment Links:\n{}", result.Stringify());
                                                                    cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.PaymentLink));
                                                                    return result;
                                                                  })!;
  }

  public Task<List<PaymentLinkItem>?> GetPaymentLinkItems(string paymentLinkId)
  {
    return _cache.GetOrCreateAsync<List<PaymentLinkItem>?>(CacheKey.From(EntityType.PaymentLinkItem, _config.Value, paymentLinkId),
                                                           async (cacheEntry) =>
                                                           {
                                                             cacheEntry.AbsoluteExpirationRelativeToNow =
                                                               _config.Value.GetCacheDuration();
                                                             _logger?.LogInformation("Fetching Payment-Link items of id: {@id}...", paymentLinkId);
                                                             var response = await ExhaustAllPages<PaymentLinkItemApiResponse>(p =>
                                                                              _chargilyPayApi.GetPaymentLinkItems(paymentLinkId, p));
                                                             var result = new List<PaymentLinkItem>();
                                                             foreach (var item in response)
                                                             {
                                                               var product = await GetProduct(item.ProductId);
                                                               result.Add(_mapper.Map<PaymentLinkItem>((item, product)));
                                                             }

                                                             _logger?.LogInformation("Fetched {count} Payment-Link items.", result.Count);
                                                             cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.PaymentLink));
                                                             cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                             return result;
                                                           });
  }

  public async IAsyncEnumerable<PaymentLinkResponse?> PaymentLinks()
  {
    var current = await GetPaymentLinks(1);
    do
    {
      foreach (var item in current.Data)
        yield return item;
      if (current.NextPage is not null) current = await GetPaymentLinks((int)current.NextPage);
    } while (current.NextPage is not null && current.NextPage != current.LastPage);
  }


  public Task<Response<PaymentLinkResponse>?> GetPaymentLink(string id)
  {
    return _cache
     .GetOrCreateAsync<Response<PaymentLinkResponse>?>(CacheKey.From(EntityType.PaymentLink, _config.Value, id),
                                                       async (cacheEntry) =>
                                                       {
                                                         cacheEntry.AbsoluteExpirationRelativeToNow =
                                                           _config.Value.GetCacheDuration();
                                                         _logger?.LogInformation("Fetching Product of id: {@id}...",
                                                                                 id);
                                                         var response = await _retryPipeline.ExecuteAsync(async (_) =>
                                                                                                            await _chargilyPayApi.GetPaymentLink(id));
                                                         var items = await _chargilyPayApi.GetPaymentLinkItems(id);
                                                         var result = _mapper.Map<Response<PaymentLinkResponse>>((response, items));

                                                         _logger?.LogDebug("Fetched Product:\n{}", result.Stringify());
                                                         cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                         return result;
                                                       });
  }

#endregion

#region Checkout

  public async Task<Response<CheckoutResponse>> CreateCheckout(Checkout checkout)
  {
    var request = _mapper.Map<CreateCheckoutRequest>(checkout);
    var validation = await GetValidator<CreateCheckoutRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Creating a new Checkout...");
    _logger?.LogDebug("Creating a new Checkout...\n{@request}", request.Stringify());
    var response = await _chargilyPayApi.CreateCheckout(request);
    var result = _mapper.Map<Response<CheckoutResponse>>(response);
    _logger?.LogInformation("Checkout created, with invoice-id {@id} and checkout-url: {@url}",
                            result.Value.InvoiceId, result.Value.CheckoutUrl);
    _logger?.LogDebug("Checkout created:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.Checkout);
    return result;
  }

  public async Task<CheckoutResponse?> CancelCheckout(string checkoutId)
  {
    _logger?.LogInformation("Cancelling checkout of id {@id}...", checkoutId);
    var response = await _chargilyPayApi.CancelCheckout(checkoutId);
    if (response is null) return null;
    _logger?.LogInformation("Cancelled checkout of id {id}", response.InvoiceId);
    OnDataStale.Invoke(EntityType.Checkout);
    return _mapper.Map<CheckoutResponse>(response);
  }

  public Task<PagedResponse<CheckoutResponse>> GetCheckouts(int page = 1, int pageSize = 50)
  {
    return
      _cache.GetOrCreateAsync<PagedResponse<CheckoutResponse>>(CacheKey.From(EntityType.Checkout, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                                               async (cacheEntry) =>
                                                               {
                                                                 cacheEntry.AbsoluteExpirationRelativeToNow =
                                                                   _config.Value.GetCacheDuration();
                                                                 _logger
                                                                 ?.LogInformation("Fetching Checkout Invoices. Page: {@pageNumber}, Page Size: {@pageSize}",
                                                                                  page, pageSize);
                                                                 var response =
                                                                   await _retryPipeline.ExecuteAsync(async (_) =>
                                                                                                       await _chargilyPayApi.GetCheckouts(page, pageSize));

                                                                 var dataItems = new List<CheckoutResponse>();
                                                                 foreach (var checkoutResponseItem in response.Data)
                                                                 {
                                                                   var checkoutItems =
                                                                     await ExhaustAllPages<CheckoutItemApiResponse>(p =>
                                                                       _chargilyPayApi.GetCheckoutItems(checkoutResponseItem.Id, p));
                                                                   var customer = await GetCustomer(checkoutResponseItem.CustomerId);
                                                                   var paymentLink = await GetPaymentLink(checkoutResponseItem.PaymentLinkId);

                                                                   var items = new List<CheckoutItem>();
                                                                   foreach (var checkoutItemResponse in checkoutItems)
                                                                   {
                                                                     var product = await GetProduct(checkoutItemResponse.ProductId);
                                                                     var mapped = _mapper.Map<CheckoutItem>((checkoutItemResponse, product));
                                                                     _cache.Set(CacheKey.From(EntityType.CheckoutItem, _config.Value, mapped.Id),
                                                                                mapped, _config.Value.GetCacheDuration());
                                                                     items.Add(mapped);
                                                                   }

                                                                   var checkoutResponse = _mapper.Map<CheckoutResponse>((items, customer, paymentLink));
                                                                   dataItems.Add(checkoutResponse);
                                                                 }

                                                                 var result = _mapper.Map<PagedResponse<CheckoutResponse>>((response, dataItems));

                                                                 _logger?.LogDebug("Fetched Checkout Invoices:\n{}", result.Stringify());
                                                                 cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Checkout));
                                                                 cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Customer));
                                                                 cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                                 cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Price));
                                                                 return result;
                                                               })!;
  }

  public Task<List<CheckoutItem>?> GetCheckoutItems(string checkoutId)
  {
    return _cache.GetOrCreateAsync<List<CheckoutItem>?>(CacheKey.From(EntityType.CheckoutItem, _config.Value, checkoutId),
                                                        async (cacheEntry) =>
                                                        {
                                                          cacheEntry.AbsoluteExpirationRelativeToNow =
                                                            _config.Value.GetCacheDuration();
                                                          _logger?.LogInformation("Fetching Checkout items of id: {@id}...", checkoutId);
                                                          var response =
                                                            await ExhaustAllPages<CheckoutItemApiResponse>(p => _chargilyPayApi.GetCheckoutItems(checkoutId, p));
                                                          var result = new List<CheckoutItem>();
                                                          foreach (var item in response)
                                                          {
                                                            var product = await GetProduct(item.ProductId);
                                                            result.Add(_mapper.Map<CheckoutItem>((item, product)));
                                                          }

                                                          _logger?.LogInformation("Fetched {count} Checkout items.", result.Count);
                                                          cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Checkout));
                                                          cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                          cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Price));
                                                          return result;
                                                        });
  }

  public Task<Response<CheckoutResponse>?> GetCheckout(string id)
  {
    return _cache.GetOrCreateAsync<Response<CheckoutResponse>?>(CacheKey.From(EntityType.CheckoutItem, _config.Value, id),
                                                                async (cacheEntry) =>
                                                                {
                                                                  cacheEntry.AbsoluteExpirationRelativeToNow = _config.Value.GetCacheDuration();
                                                                  _logger?.LogInformation("Fetching Checkout of id: {@id}...", id);
                                                                  var response =
                                                                    await _retryPipeline.ExecuteAsync(async (_) => await _chargilyPayApi.GetCheckout(id));
                                                                  var customer = GetCustomer(response.CustomerId);
                                                                  var paymentLink = await GetPaymentLink(response.PaymentLinkId);
                                                                  var items = await GetCheckoutItems(id);
                                                                  var result =
                                                                    _mapper.Map<Response<CheckoutResponse>>((response, items, customer, paymentLink));
                                                                  _logger?.LogDebug("Fetched Checkout:\n{@data}", result.Stringify());
                                                                  cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Checkout));
                                                                  cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                                  cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Customer));
                                                                  return result;
                                                                });
  }

  public async IAsyncEnumerable<CheckoutResponse?> Checkouts()
  {
    var current = await GetCheckouts(1);
    do
    {
      foreach (var item in current.Data)
        yield return item;
      if (current.NextPage is not null) current = await GetCheckouts((int)current.NextPage);
    } while (current.NextPage is not null && current.NextPage != current.LastPage);
  }

#endregion

#region Price

  public async Task<Response<Price>> AddPrice(CreatePrice price)
  {
    var request = _mapper.Map<CreatePriceRequest>(price);
    var validation = await GetValidator<CreatePriceRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Creating a new Price...");
    _logger?.LogDebug("Creating a new Price...\n{@request}", request.Stringify());
    var response = await _chargilyPayApi.CreatePrice(request);
    var product = await GetProduct(response.ProductId);
    var result = _mapper.Map<Response<Price>>((response, product));
    _logger?.LogDebug("Price created:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.Price);
    return result;
  }

  public async Task<Response<Price>> UpdatePrice(UpdatePrice update)
  {
    var request = _mapper.Map<UpdatePriceRequest>(update);
    var validation = await GetValidator<UpdatePriceRequest>()
                      .ValidateAsync(request);
    if (!validation.IsValid)
      validation.LogValidationErrorsAndThrow(_logger);

    _logger?.LogInformation("Update Price of id: {@}...", request.Id);
    _logger?.LogDebug("Update Price of id: {@} with:\n{@request}", request.Id, request.Stringify(true));
    var response = await _chargilyPayApi.UpdatePrice(request.Id, request);
    var product = await GetProduct(response.ProductId);
    var result = _mapper.Map<Response<Price>>((response, product));
    _logger?.LogDebug("Price updated:\n{@response}", result.Stringify());
    OnDataStale.Invoke(EntityType.Price);
    return result;
  }

  public Task<PagedResponse<Price>> GetPrices(int page = 1, int pageSize = 50)
  {
    return
      _cache.GetOrCreateAsync<PagedResponse<Price>>(CacheKey.From(EntityType.Price, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                                    async (cacheEntry) =>
                                                    {
                                                      cacheEntry.AbsoluteExpirationRelativeToNow = _config.Value.GetCacheDuration();
                                                      _logger?.LogInformation("Fetching Prices. Page: {@pageNumber}, Page Size: {@pageSize}...",
                                                                              page, pageSize);
                                                      var response =
                                                        await _retryPipeline.ExecuteAsync(async (_) => await _chargilyPayApi.GetPrices(page, pageSize));
                                                      var prices = new List<Price>();
                                                      foreach (var item in response.Data)
                                                      {
                                                        var product = await GetProduct(item.ProductId);
                                                        var mapped = _mapper.Map<Price>((item, product));
                                                        prices.Add(mapped);
                                                      }

                                                      var result = _mapper.Map<PagedResponse<Price>>((response, prices));
                                                      _logger?.LogDebug("Fetched Prices:\n{}", result.Stringify());
                                                      cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                      cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Price));
                                                      return result;
                                                    })!;
  }

  public async IAsyncEnumerable<Price?> Prices()
  {
    var current = await GetPrices(1);
    do
    {
      foreach (var item in current.Data)
        yield return item;
      if (current.NextPage is not null) current = await GetPrices((int)current.NextPage);
    } while (current.NextPage is not null && current.NextPage != current.LastPage);
  }

  public Task<Response<Price>?> GetPrice(string id)
  {
    return _cache.GetOrCreateAsync<Response<Price>?>(CacheKey.From(EntityType.Price, _config.Value, id),
                                                     async (cacheEntry) =>
                                                     {
                                                       cacheEntry.AbsoluteExpirationRelativeToNow = _config.Value.GetCacheDuration();
                                                       _logger?.LogInformation("Fetching Price of id: {@id}...", id);
                                                       var response = await _retryPipeline.ExecuteAsync(async (_) => await _chargilyPayApi.GetPrice(id));
                                                       var product = await GetProduct(response.ProductId);
                                                       var result = _mapper.Map<Response<Price>>((response, product));
                                                       _logger?.LogDebug("Fetched Price:\n{@data}", result.Stringify());
                                                       cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Product));
                                                       cacheEntry.AddExpirationToken(CreateCacheExpiration(EntityType.Price));
                                                       return result;
                                                     });
  }

#endregion


}