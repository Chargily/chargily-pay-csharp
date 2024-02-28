using System.Text.Json;
using AutoMapper;
using Chargily.Pay.V2.Internal.Endpoints;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Models;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace Chargily.Pay.V2.Internal;

internal class ResilientChargilyClient
{
    private readonly IChargilyApi _chargilyApi;
    private readonly ServiceProvider _provider;
    private readonly IOptions<ChargilyConfig> _config;
    private readonly IMemoryCache _cache;
    private readonly ResiliencePipeline _retryPipeline;
    private readonly ILogger<ResilientChargilyClient> _logger;
    private readonly IMapper _mapper;

    private IValidator<T> GetValidator<T>() => _provider.GetRequiredService<IValidator<T>>();

    internal ResilientChargilyClient(IMemoryCache cache,
                                     IMapper mapper,
                                     ILogger<ResilientChargilyClient> logger,
                                     IChargilyApi chargilyApi,
                                     ServiceProvider provider,
                                     IOptions<ChargilyConfig> config)
    {
        _mapper = mapper;
        _logger = logger;
        _cache = cache;
        _config = config;
        _chargilyApi = chargilyApi;
        _provider = provider;

        _retryPipeline = new ResiliencePipelineBuilder()
                        .AddRetry(new RetryStrategyOptions()
                                  {
                                      DelayGenerator = (attempt) =>
                                                           ValueTask.FromResult<TimeSpan?>(_config.Value
                                                                           .DelayPerRetryCalculator
                                                                          ?.Invoke(attempt.AttemptNumber) ??
                                                                        TimeSpan.FromMilliseconds(500 * attempt
                                                                                        .AttemptNumber)),
                                      OnRetry = (ctx) =>
                                                {
                                                    logger
                                                      ?.LogInformation("Failed, Attempt number: {@number}/{@max}, retrying after {seconds:N3} seconds.",
                                                                       ctx.AttemptNumber,
                                                                       _config.Value.MaxRetriesOnFailure,
                                                                       ctx.RetryDelay.TotalSeconds);
                                                    logger?.LogError("Failed with Exception: {@ex}",
                                                                     ctx.Outcome.Exception?.ToString());
                                                    return ValueTask.CompletedTask;
                                                },
                                      MaxRetryAttempts = _config.Value.MaxRetriesOnFailure,
                                  })
                        .Build();
    }

#region Balance

    public Task<IReadOnlyList<Wallet>?> GetBalance()
    {
        return _cache.GetOrCreateAsync<IReadOnlyList<Wallet>>(CacheKey.From(EntityType.Balance, _config.Value),
                                                              async (cacheEntry) =>
                                                              {
                                                                  cacheEntry.AbsoluteExpirationRelativeToNow =
                                                                      _config.Value.GetCacheDuration();
                                                                  _logger
                                                                    ?.LogInformation("Refreshing Account Balance...");
                                                                  var response = await _chargilyApi.GetBalance();
                                                                  var wallets =
                                                                      _mapper.Map<IReadOnlyList<Wallet>>(response);
                                                                  _logger?.LogDebug("Fetched Balance:\n{}",
                                                                           JsonSerializer.Serialize(wallets,
                                                                                    new JsonSerializerOptions()
                                                                                    { WriteIndented = true }));
                                                                  return wallets;
                                                              });
    }

#endregion

#region Product

    public async Task<Response<Product>> AddProduct(Product product)
    {
        var request = _mapper.Map<CreateProductRequest>(product);
        var validation = await GetValidator<CreateProductRequest>()
                            .ValidateAsync(request);
        if (!validation.IsValid)
            validation.LogValidationErrorsAndThrow(_logger);

        _logger?.LogInformation("Creating a new Product... {@name}", product.Name);
        _logger?.LogDebug("Creating a new Product...\n{@request}", request.Stringify());
        var response = await _chargilyApi.CreateProduct(request);
        var result = _mapper.Map<Response<Product>>(response);
        _logger?.LogDebug("Product created:\n{@response}", result.Stringify());
        return result;
    }

    public Task<PagedResponse<Product>> GetProducts(int page = 1, int pageSize = 100)
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
                                                        var response = await _chargilyApi.GetProducts(page, pageSize);
                                                        var result = _mapper.Map<PagedResponse<Product>>(response);
                                                        foreach (var item in result.Data)
                                                        {
                                                            _cache
                                                               .Set(CacheKey.From(EntityType.Product, _config.Value, item.Id),
                                                                    item, _config.Value.GetCacheDuration());
                                                        }

                                                        _logger?.LogDebug("Fetched Products:\n{}",
                                                                          JsonSerializer.Serialize(result,
                                                                                   new JsonSerializerOptions()
                                                                                   { WriteIndented = true }));
                                                        return result;
                                                    })!;
    }

    public async IAsyncEnumerable<Product?> Products()
    {
        var current = await GetProducts(1);
        do
        {
            foreach (var item in current.Data)
                yield return _mapper.Map<Product>(item);
            if (current.NextPage is not null) current = await GetProducts((int)current.NextPage);
        } while (current.NextPage is not null && current.NextPage != current.LastPage);
    }

#endregion

#region Customer

    public async Task<Response<Customer>> AddCustomer(Customer customer)
    {
        var request = _mapper.Map<CreateCustomerRequest>(customer);
        var validation = await GetValidator<CreateCustomerRequest>()
                            .ValidateAsync(request);
        if (!validation.IsValid)
            validation.LogValidationErrorsAndThrow(_logger);

        _logger?.LogInformation("Creating a new Customer... {@name}", customer.Name);
        _logger?.LogDebug("Creating a new Customer...\n{@request}", request.Stringify());
        var response = await _chargilyApi.CreateCustomer(request);
        var result = _mapper.Map<Response<Customer>>(response);
        _logger?.LogDebug("Customer created:\n{@response}", result.Stringify());
        return result;
    }

    public Task<PagedResponse<Customer>> GetCustomers(int page = 1, int pageSize = 100)
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
                                                         var response = await _chargilyApi.GetCustomers(page, pageSize);
                                                         var result = _mapper.Map<PagedResponse<Customer>>(response);
                                                         foreach (var item in result.Data)
                                                         {
                                                             _cache
                                                                .Set(CacheKey.From(EntityType.Customer, _config.Value, item.Id),
                                                                     item, _config.Value.GetCacheDuration());
                                                         }

                                                         _logger?.LogDebug("Fetched Customers:\n{}",
                                                                           JsonSerializer.Serialize(result,
                                                                                    new JsonSerializerOptions()
                                                                                    { WriteIndented = true }));
                                                         return result;
                                                     })!;
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

    public async Task<Response<PaymentLinkResponse>> CreatePaymentLink(PaymentLink paymentLink)
    {
        var request = _mapper.Map<CreatePaymentLinkRequest>(paymentLink);
        var validation = await GetValidator<CreatePaymentLinkRequest>()
                            .ValidateAsync(request);
        if (!validation.IsValid)
            validation.LogValidationErrorsAndThrow(_logger);

        _logger?.LogInformation("Creating a new Payment-link...");
        _logger?.LogDebug("Creating a new Payment-link...\n{@request}", request.Stringify());
        var response = await _chargilyApi.CreatePaymentLink(request);
        var result = _mapper.Map<Response<PaymentLinkResponse>>(response);
        _logger?.LogInformation("Payment-link created, with id {@id} and url: {@url}",
                                result.Value.Id, result.Value.Url);
        _logger?.LogDebug("Payment-link created:\n{@response}", result.Stringify());
        return result;
    }

    public Task<PagedResponse<PaymentLinkResponse>> GetPaymentLinks(int page = 1, int pageSize = 100)
    {
        return
            _cache.GetOrCreateAsync<PagedResponse<PaymentLinkResponse>>(CacheKey.From(EntityType.PaymentLink, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                                                        async (cacheEntry) =>
                                                                        {
                                                                            cacheEntry.AbsoluteExpirationRelativeToNow =
                                                                                _config.Value.GetCacheDuration();
                                                                            _logger
                                                                              ?.LogInformation("Fetching Payment Links. Page: {@pageNumber}, Page Size: {@pageSize}",
                                                                                    page, pageSize);
                                                                            var response =
                                                                                await _chargilyApi.GetPaymentLinks(page, pageSize);
                                                                            var result = _mapper.Map<PagedResponse<PaymentLinkResponse>>(response);
                                                                            foreach (var item in result.Data)
                                                                            {
                                                                                _cache
                                                                                   .Set(CacheKey.From(EntityType.PaymentLink, _config.Value, item.Id),
                                                                                        item, _config.Value.GetCacheDuration());
                                                                            }

                                                                            _logger?.LogDebug("Fetched Payment Links:\n{}",
                                                                                JsonSerializer.Serialize(result,
                                                                                    new JsonSerializerOptions()
                                                                                    { WriteIndented = true }));
                                                                            return result;
                                                                        })!;
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

#endregion

#region Checkout

    public async Task<Response<CheckoutResponse>> CreateCheckout(PaymentLink checkout)
    {
        var request = _mapper.Map<CreatePaymentLinkRequest>(checkout);
        var validation = await GetValidator<CreatePaymentLinkRequest>()
                            .ValidateAsync(request);
        if (!validation.IsValid)
            validation.LogValidationErrorsAndThrow(_logger);

        _logger?.LogInformation("Creating a new Checkout...");
        _logger?.LogDebug("Creating a new Checkout...\n{@request}", request.Stringify());
        var response = await _chargilyApi.CreatePaymentLink(request);
        var result = _mapper.Map<Response<CheckoutResponse>>(response);
        _logger?.LogInformation("Checkout created, with invoice-id {@id} and checkout-url: {@url}",
                                result.Value.InvoiceId, result.Value.CheckoutUrl);
        _logger?.LogDebug("Checkout created:\n{@response}", result.Stringify());
        return result;
    }

    public Task<PagedResponse<CheckoutResponse>> GetCheckouts(int page = 1, int pageSize = 100)
    {
        return
            _cache
               .GetOrCreateAsync<
                PagedResponse<CheckoutResponse>>(CacheKey.From(EntityType.Checkout, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                         async (cacheEntry) =>
                                         {
                                             cacheEntry.AbsoluteExpirationRelativeToNow =
                                                 _config.Value.GetCacheDuration();
                                             _logger
                                               ?.LogInformation("Fetching Checkout Invoices. Page: {@pageNumber}, Page Size: {@pageSize}",
                                                                page, pageSize);
                                             var response = await _chargilyApi.GetCheckouts(page, pageSize);
                                             var result = _mapper.Map<PagedResponse<CheckoutResponse>>(response);
                                             foreach (var item in result.Data)
                                             {
                                                 _cache
                                                    .Set(CacheKey.From(EntityType.Checkout, _config.Value, item.Id),
                                                         item, _config.Value.GetCacheDuration());
                                             }

                                             _logger?.LogDebug("Fetched Checkout Invoices:\n{}",
                                                               JsonSerializer.Serialize(result,
                                                                        new JsonSerializerOptions()
                                                                        { WriteIndented = true }));
                                             return result;
                                         })!;
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

    public async Task<Response<Price>> AddPrice(Price price)
    {
        var request = _mapper.Map<CreatePriceRequest>(price);
        var validation = await GetValidator<CreatePriceRequest>()
                            .ValidateAsync(request);
        if (!validation.IsValid)
            validation.LogValidationErrorsAndThrow(_logger);

        _logger?.LogInformation("Creating a new Price...");
        _logger?.LogDebug("Creating a new Price...\n{@request}", request.Stringify());
        var response = await _chargilyApi.CreatePrice(request);
        var result = _mapper.Map<Response<Price>>(response);
        _logger?.LogDebug("Price created:\n{@response}", result.Stringify());
        return result;
    }
    public Task<PagedResponse<Price>> GetPrices(int page = 1, int pageSize = 100)
    {
        return
            _cache.GetOrCreateAsync<PagedResponse<Price>>(CacheKey.From(EntityType.Price, _config.Value, "page", page.ToString(), "page_size", pageSize.ToString()),
                                                  async (cacheEntry) =>
                                                  {
                                                      cacheEntry.AbsoluteExpirationRelativeToNow =
                                                          _config.Value.GetCacheDuration();
                                                      _logger
                                                        ?.LogInformation("Fetching Prices. Page: {@pageNumber}, Page Size: {@pageSize}...",
                                                                         page, pageSize);
                                                      var response = await _chargilyApi.GetPrices(page, pageSize);
                                                      var result = _mapper.Map<PagedResponse<Price>>(response);
                                                      foreach (var item in result.Data)
                                                      {
                                                          _cache
                                                             .Set(CacheKey.From(EntityType.Price, _config.Value, item.Id),
                                                                  item, _config.Value.GetCacheDuration());
                                                      }

                                                      _logger?.LogDebug("Fetched Prices:\n{}",
                                                                        JsonSerializer.Serialize(result,
                                                                                 new JsonSerializerOptions()
                                                                                 { WriteIndented = true }));
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

#endregion
}