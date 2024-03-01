using AutoMapper;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class CheckoutProfile : Profile
{
  public CheckoutProfile()
  {
    var languages = new Dictionary<LocaleType, string>()
                    {
                      { LocaleType.Arabic, "ar" },
                      { LocaleType.English, "en" },
                      { LocaleType.French, "fr" },
                    };
    var locales = new Dictionary<string, LocaleType>()
                  {
                    { "ar", LocaleType.Arabic },
                    { "en", LocaleType.English },
                    { "fr", LocaleType.French },
                  };
    CreateMap<Checkout, CreateCheckoutRequest>()
     .ConstructUsing((req, ctx) => new CreateCheckoutRequest()
                                   {
                                     Metadata = req.Metadata,
                                     Currency = ctx.Mapper.Map<string>(req.Currency),
                                     Amount = req.Amount,
                                     CustomerId = req.CustomerId,
                                     Description = req.Description,
                                     Items = req.Items?.Select(x => new CheckoutItemRequest()
                                                                    {
                                                                      Price = x.Amount.ToString(),
                                                                      Quantity = x.Quantity
                                                                    })
                                                .ToArray(),
                                     Language = languages[req.Language],
                                     OnFailureRedirectUrl = req.OnFailureRedirectUrl.ToString(),
                                     OnSuccessRedirectUrl = req.OnSuccessRedirectUrl.ToString(),
                                     PaymentMethod = Enum.GetName(req.PaymentMethod)!,
                                     PassFeesToCustomer = req.PassFeesToCustomer,
                                     WebhookEndpointUrl = req.WebhookEndpointUrl.ToString(),
                                   });
    CreateMap<(CreateCheckoutRequest Request, List<CheckoutItem> Items, Customer? Customer, PaymentLink? PaymentLink), Checkout>()
     .ConstructUsing((req, ctx) => new Checkout()
                                   {
                                     Metadata = req.Request.Metadata,
                                     Currency = ctx.Mapper.Map<Currency>(req.Request.Currency),
                                     Amount = req.Request.Amount,
                                     CustomerId = req.Request.CustomerId,
                                     Description = req.Request.Description,
                                     Items = req.Items,
                                     Language = locales[req.Request.Language],
                                     OnFailureRedirectUrl = new Uri(req.Request.OnFailureRedirectUrl),
                                     OnSuccessRedirectUrl = new Uri(req.Request.OnSuccessRedirectUrl),
                                     PaymentMethod = Enum.Parse<PaymentMethod>(req.Request.PaymentMethod)!,
                                     PassFeesToCustomer = req.Request.PassFeesToCustomer,
                                     WebhookEndpointUrl = new Uri(req.Request.WebhookEndpointUrl),
                                   });

    CreateMap<(CheckoutApiResponse Response, List<CheckoutItem> Items, Customer? Customer, PaymentLink? PaymentLink), Response<CheckoutResponse>>()
     .ConstructUsing((res, ctx) => new Response<CheckoutResponse>()
                                   {
                                     Id = res.Response.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(res.Response.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(res.Response.LastUpdatedAt),
                                     IsLiveMode = res.Response.IsLiveMode,
                                     Value = new CheckoutResponse()
                                             {
                                               Id = res.Response.Id,
                                               Metadata = res.Response.Metadata,
                                               Amount = res.Response.Amount,
                                               Currency = ctx.Mapper.Map<Currency>(res.Response.Currency),
                                               CheckoutUrl = new Uri(res.Response.CheckoutUrl),
                                               WebhookEndpointUrl = new Uri(res.Response.CheckoutUrl),
                                               OnFailureRedirectUrl = new Uri(res.Response.CheckoutUrl),
                                               OnSuccessRedirectUrl = new Uri(res.Response.CheckoutUrl),
                                               Customer = res.Customer,
                                               PaymentLink = res.PaymentLink,
                                               PaymentLinkId = res.Response.PaymentLinkId,
                                               CustomerId = res.Response.CustomerId,
                                               InvoiceId = res.Response.InvoiceId,
                                               Fees = res.Response.Fees,
                                               Status = Enum.Parse<CheckoutStatus>(res.Response.Status),
                                               Language = locales[res.Response.Language],
                                               Description = res.Response.Description,
                                               Items = res.Items,
                                               PaymentMethod = Enum.Parse<PaymentMethod>(res.Response.PaymentMethod),
                                               PassFeesToCustomer = res.Response.PassFeesToCustomer
                                             },
                                   });

    CreateMap<(CheckoutApiResponse Response, List<CheckoutItem> Items, Customer? Customer, PaymentLink? PaymentLink), CheckoutResponse>()
     .ConstructUsing((res, ctx) => new CheckoutResponse()
                                   {
                                     Id = res.Response.Id,
                                     Metadata = res.Response.Metadata,
                                     Amount = res.Response.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Response.Currency),
                                     CheckoutUrl = new Uri(res.Response.CheckoutUrl),
                                     WebhookEndpointUrl = new Uri(res.Response.CheckoutUrl),
                                     OnFailureRedirectUrl = new Uri(res.Response.CheckoutUrl),
                                     OnSuccessRedirectUrl = new Uri(res.Response.CheckoutUrl),
                                     Customer = res.Customer,
                                     PaymentLink = res.PaymentLink,
                                     PaymentLinkId = res.Response.PaymentLinkId,
                                     CustomerId = res.Response.CustomerId,
                                     InvoiceId = res.Response.InvoiceId,
                                     Fees = res.Response.Fees,
                                     Status = Enum.Parse<CheckoutStatus>(res.Response.Status),
                                     Language = locales[res.Response.Language],
                                     Description = res.Response.Description,
                                     Items = res.Items,
                                     PaymentMethod = Enum.Parse<PaymentMethod>(res.Response.PaymentMethod),
                                     PassFeesToCustomer = res.Response.PassFeesToCustomer
                                   });

    CreateMap<(PagedApiResponse<CheckoutApiResponse> Response, List<CheckoutResponse> Items), PagedResponse<CheckoutResponse>>()
     .ConstructUsing((res, ctx) => new PagedResponse<CheckoutResponse>()
                                   {
                                     Data = res.Items,
                                     CurrentPage = res.Response.CurrentPage,
                                     FirstPage = res.Response.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.Response.LastPageUrl.GetPageOrDefault(1),
                                     NextPage = res.Response.NextPageUrl?.GetPage(),
                                     PreviousPage = res.Response.NextPageUrl?.GetPage(),
                                     PageSize = res.Response.PageSize,
                                     Total = res.Response.Total
                                   });

    CreateMap<(CheckoutItemApiResponse Response, Product? Product), CheckoutItem>()
     .ConstructUsing((res, ctx) => new CheckoutItem()
                                   {
                                     Id = res.Response.Id,
                                     Amount = res.Response.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Response.Currency),
                                     Metadata = res.Response.Metadata,
                                     Product = res.Product,
                                     ProductId = res.Response.ProductId,
                                     Quantity = res.Response.Quantity
                                   });
  }
}

public class PaymentLinkProfile : Profile
{
  public PaymentLinkProfile()
  {
    var languages = new Dictionary<LocaleType, string>()
                    {
                      { LocaleType.Arabic, "ar" },
                      { LocaleType.English, "en" },
                      { LocaleType.French, "fr" },
                    };
    var locales = new Dictionary<string, LocaleType>()
                  {
                    { "ar", LocaleType.Arabic },
                    { "en", LocaleType.English },
                    { "fr", LocaleType.French },
                  };
    CreateMap<PaymentLink, CreatePaymentLinkRequest>()
     .ConstructUsing((req, ctx) => new CreatePaymentLinkRequest()
                                   {
                                     Metadata = req.Metadata,
                                     PassFeesToCustomer = req.PassFeesToCustomer,
                                     Language = languages[req.Language],
                                     CompletionMessage = req.CompletionMessage,
                                     IsActive = req.IsActive,
                                     Name = req.Name
                                   });
    CreateMap<UpdatePaymentLink, UpdatePaymentLinkRequest>()
     .ConstructUsing((req, ctx) => new UpdatePaymentLinkRequest()
                                   {
                                     Id = req.Id,
                                     Metadata = req.Metadata,
                                     PassFeesToCustomer = req.PassFeesToCustomer,
                                     Language = languages[(LocaleType)req.Language],
                                     CompletionMessage = req.CompletionMessage,
                                     IsActive = req.IsActive,
                                     Name = req.Name
                                   });

    CreateMap<(CreateCheckoutRequest Request, List<CheckoutItem> Items, Customer? Customer, PaymentLink? PaymentLink), Checkout>()
     .ConstructUsing((req, ctx) => new Checkout()
                                   {
                                     Metadata = req.Request.Metadata,
                                     Currency = ctx.Mapper.Map<Currency>(req.Request.Currency),
                                     Amount = req.Request.Amount,
                                     CustomerId = req.Request.CustomerId,
                                     Description = req.Request.Description,
                                     Items = req.Items,
                                     Language = locales[req.Request.Language],
                                     OnFailureRedirectUrl = new Uri(req.Request.OnFailureRedirectUrl),
                                     OnSuccessRedirectUrl = new Uri(req.Request.OnSuccessRedirectUrl),
                                     PaymentMethod = Enum.Parse<PaymentMethod>(req.Request.PaymentMethod)!,
                                     PassFeesToCustomer = req.Request.PassFeesToCustomer,
                                     WebhookEndpointUrl = new Uri(req.Request.WebhookEndpointUrl),
                                   });

    CreateMap<(PaymentLinkApiResponse Response, List<PaymentLinkItem> Items), Response<PaymentLinkResponse>>()
     .ConstructUsing((res, ctx) => new Response<PaymentLinkResponse>()
                                   {
                                     Id = res.Response.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(res.Response.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(res.Response.LastUpdatedAt),
                                     IsLiveMode = res.Response.IsLiveMode,
                                     Value = new PaymentLinkResponse()
                                             {
                                               Id = res.Response.Id,
                                               Metadata = res.Response.Metadata,
                                               Language = locales[res.Response.Language],
                                               Items = res.Items,
                                               PassFeesToCustomer = res.Response.PassFeesToCustomer,
                                               CompletionMessage = res.Response.CompletionMessage,
                                               IsActive = res.Response.IsActive,
                                               Name = res.Response.Name,
                                               Url = new Uri(res.Response.Url),
                                             },
                                   });
    CreateMap<(PaymentLinkApiResponse Response, List<PaymentLinkItem> Items), PaymentLinkResponse>()
     .ConstructUsing((res, ctx) => new PaymentLinkResponse()
                                   {
                                     Id = res.Response.Id,
                                     Metadata = res.Response.Metadata,
                                     Language = locales[res.Response.Language],
                                     Items = res.Items,
                                     PassFeesToCustomer = res.Response.PassFeesToCustomer,
                                     CompletionMessage = res.Response.CompletionMessage,
                                     IsActive = res.Response.IsActive,
                                     Name = res.Response.Name,
                                     Url = new Uri(res.Response.Url),
                                   });

    CreateMap<(PagedApiResponse<PaymentLinkApiResponse> Response, List<PaymentLinkResponse> Items), PagedResponse<PaymentLinkResponse>>()
     .ConstructUsing((res, ctx) => new PagedResponse<PaymentLinkResponse>()
                                   {
                                     Data = res.Items,
                                     CurrentPage = res.Response.CurrentPage,
                                     FirstPage = res.Response.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.Response.LastPageUrl.GetPageOrDefault(1),
                                     NextPage = res.Response.NextPageUrl?.GetPage(),
                                     PreviousPage = res.Response.NextPageUrl?.GetPage(),
                                     PageSize = res.Response.PageSize,
                                     Total = res.Response.Total
                                   });

    CreateMap<(PaymentLinkItemApiResponse Response, Product? Product), PaymentLinkItem>()
     .ConstructUsing((res, _) => new PaymentLinkItem()
                                 {
                                   Metadata = res.Response.Metadata,
                                   Amount = res.Response.Amount,
                                   Currency = Enum.Parse<Currency>(res.Response.Currency),
                                   Product = res.Product,
                                   Quantity = res.Response.Quantity,
                                   ProductId = res.Response.ProductId,
                                   AdjustableQuantity = res.Response.AdjustableQuantity,
                                 });
  }
}