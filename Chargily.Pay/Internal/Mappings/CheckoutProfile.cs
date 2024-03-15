using AutoMapper;
using Chargily.Pay.Internal.Requests;
using Chargily.Pay.Internal.Responses;
using Chargily.Pay.Models;
using Checkout = Chargily.Pay.Models.Checkout;
using CheckoutItem = Chargily.Pay.Models.CheckoutItem;
using CheckoutPriceItem = Chargily.Pay.Models.CheckoutPriceItem;
using CheckoutResponse = Chargily.Pay.Models.CheckoutResponse;
using CheckoutStatus = Chargily.Pay.Models.CheckoutStatus;
using Currency = Chargily.Pay.Models.Currency;
using PaymentLinkResponse = Chargily.Pay.Models.PaymentLinkResponse;
using PaymentMethod = Chargily.Pay.Models.PaymentMethod;

namespace Chargily.Pay.Internal.Mappings;

public class CheckoutProfile : Profile
{
  public CheckoutProfile()
  {

    CreateMap<CheckoutPriceItem, CheckoutItemPriceRequest>()
     .ConstructUsing((req, ctx) => new CheckoutItemPriceRequest()
                                   {
                                     Quantity = req.Quantity,
                                     PriceId = req.PriceId,
                                   });
    CreateMap<Checkout, CreateCheckoutRequest>()
     .ConstructUsing((req, ctx) => new CreateCheckoutRequest()
                                   {
                                     Metadata = req.Metadata,
                                     Currency = ctx.Mapper.Map<string>(req.Currency),
                                     Amount = req.Amount,
                                     CustomerId = req.CustomerId,
                                     Description = req.Description,
                                     Items = ctx.Mapper.Map<CheckoutItemPriceRequest[]>(req.Items),
                                     Language = Language.GetLanguage(req.Language) ?? "en",
                                     OnFailureRedirectUrl = req.OnFailureRedirectUrl.ToString(),
                                     OnSuccessRedirectUrl = req.OnSuccessRedirectUrl.ToString(),
                                     PaymentMethod = Enum.GetName(req.PaymentMethod)!,
                                     PassFeesToCustomer = req.PassFeesToCustomer,
                                     WebhookEndpointUrl = req.WebhookEndpointUrl.ToString(),
                                   })
     .AfterMap((_, result) =>
               {
                 if (result.Items is { Length: 0 })
                   result.Items = null;
               });
    CreateMap<(CreateCheckoutRequest Request, List<CheckoutPriceItem> Items, Models.Customer? Customer, Models.PaymentLink? PaymentLink), Checkout>()
     .ConstructUsing((req, ctx) => new Checkout()
                                   {
                                     Metadata = req.Request.Metadata,
                                     Currency = ctx.Mapper.Map<Currency>(req.Request.Currency),
                                     Amount = req.Request.Amount,
                                     CustomerId = req.Request.CustomerId,
                                     Description = req.Request.Description,
                                     Items = req.Items,
                                     Language = Language.GetLocalType(req.Request.Language) ?? LocaleType.English,
                                     OnFailureRedirectUrl = new Uri(req.Request.OnFailureRedirectUrl),
                                     OnSuccessRedirectUrl = new Uri(req.Request.OnSuccessRedirectUrl),
                                     PaymentMethod = Enum.Parse<PaymentMethod>(req.Request.PaymentMethod)!,
                                     PassFeesToCustomer = req.Request.PassFeesToCustomer,
                                     WebhookEndpointUrl = new Uri(req.Request.WebhookEndpointUrl),
                                   });

    CreateMap < CheckoutApiResponse, Models.Response<CheckoutResponse>>()
     .ConstructUsing((res, ctx) => new Models.Response<CheckoutResponse>()
                                   {
                                     Id = res.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.LastUpdatedAt),
                                     IsLiveMode = res.IsLiveMode,
                                     Value = new CheckoutResponse()
                                             {
                                               Id = res.Id,
                                               Metadata = res.Metadata,
                                               Amount = res.Amount,
                                               Currency = ctx.Mapper.Map<Currency>(res.Currency),
                                               CheckoutUrl =  res.CheckoutUrl is not null ? new Uri(res.CheckoutUrl) : null,
                                               WebhookEndpointUrl = new Uri(res.WebhookEndpointUrl),
                                               OnFailureRedirectUrl = new Uri(res.OnFailureRedirectUrl),
                                               OnSuccessRedirectUrl = new Uri(res.OnSuccessRedirectUrl),
                                               PaymentLinkId = res.PaymentLinkId,
                                               CustomerId = res.CustomerId,
                                               InvoiceId = res.InvoiceId,
                                               Fees = res.Fees,
                                               Status = Enum.GetValues<CheckoutStatus>().FirstOrDefault(x => x.ToString().Equals(res.Status, StringComparison.OrdinalIgnoreCase)),
                                               Language = Language.GetLocalType(res.Language) ?? LocaleType.English,
                                               Description = res.Description,
                                               PaymentMethod = Enum.GetValues<PaymentMethod>().FirstOrDefault(x => x.ToString().Equals(res.PaymentMethod, StringComparison.OrdinalIgnoreCase)),
                                               PassFeesToCustomer = res.PassFeesToCustomer
                                             },
                                   });
    
      CreateMap < CheckoutApiResponse, CheckoutResponse>()
     .ConstructUsing((res, ctx) => new CheckoutResponse()
                                   {
                                     Id = res.Id,
                                     Metadata = res.Metadata,
                                     Amount = res.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Currency),
                                     CheckoutUrl =  res.CheckoutUrl is not null ? new Uri(res.CheckoutUrl) : null,
                                     WebhookEndpointUrl = new Uri(res.WebhookEndpointUrl),
                                     OnFailureRedirectUrl = new Uri(res.OnFailureRedirectUrl),
                                     OnSuccessRedirectUrl = new Uri(res.OnSuccessRedirectUrl),
                                     PaymentLinkId = res.PaymentLinkId,
                                     CustomerId = res.CustomerId,
                                     InvoiceId = res.InvoiceId,
                                     Fees = res.Fees,
                                     Status = Enum.GetValues<CheckoutStatus>().FirstOrDefault(x => x.ToString().Equals(res.Status, StringComparison.OrdinalIgnoreCase)),
                                     Language = Language.GetLocalType(res.Language) ?? LocaleType.English,
                                     Description = res.Description,
                                     PaymentMethod = Enum.GetValues<PaymentMethod>().FirstOrDefault(x => x.ToString().Equals(res.PaymentMethod, StringComparison.OrdinalIgnoreCase)),
                                     PassFeesToCustomer = res.PassFeesToCustomer
                                   });

    CreateMap<(CheckoutApiResponse Response, List<CheckoutItem>? Items, Models.Customer? Customer, PaymentLinkResponse? PaymentLink), Models.Response<CheckoutResponse>>()
     .ConstructUsing((res, ctx) => new Models.Response<CheckoutResponse>()
                                   {
                                     Id = res.Response.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Response.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Response.LastUpdatedAt),
                                     IsLiveMode = res.Response.IsLiveMode,
                                     Value = new CheckoutResponse()
                                             {
                                               Id = res.Response.Id,
                                               Metadata = res.Response.Metadata,
                                               Amount = res.Response.Amount,
                                               Currency = ctx.Mapper.Map<Currency>(res.Response.Currency),
                                               CheckoutUrl =  res.Response.CheckoutUrl is not null ? new Uri(res.Response.CheckoutUrl) : null,
                                               WebhookEndpointUrl = new Uri(res.Response.WebhookEndpointUrl),
                                               OnFailureRedirectUrl = new Uri(res.Response.OnFailureRedirectUrl),
                                               OnSuccessRedirectUrl = new Uri(res.Response.OnSuccessRedirectUrl),
                                               Customer = res.Customer,
                                               PaymentLink = res.PaymentLink,
                                               PaymentLinkId = res.Response.PaymentLinkId,
                                               CustomerId = res.Response.CustomerId,
                                               InvoiceId = res.Response.InvoiceId,
                                               Fees = res.Response.Fees,
                                               Status = Enum.GetValues<CheckoutStatus>().FirstOrDefault(x => x.ToString().Equals(res.Response.Status, StringComparison.OrdinalIgnoreCase)),
                                               Language = Language.GetLocalType(res.Response.Language) ?? LocaleType.English,
                                               Description = res.Response.Description,
                                               Items = res.Items,
                                               PaymentMethod = Enum.GetValues<PaymentMethod>().FirstOrDefault(x => x.ToString().Equals(res.Response.PaymentMethod, StringComparison.OrdinalIgnoreCase)),
                                               PassFeesToCustomer = res.Response.PassFeesToCustomer
                                             },
                                   });

    CreateMap<(CheckoutApiResponse Response, List<CheckoutItem>? Items, Models.Customer? Customer, PaymentLinkResponse? PaymentLink), CheckoutResponse>()
     .ConstructUsing((res, ctx) => new CheckoutResponse()
                                   {
                                     Id = res.Response.Id,
                                     Metadata = res.Response.Metadata,
                                     Amount = res.Response.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Response.Currency),
                                     CheckoutUrl = res.Response.CheckoutUrl is not null ? new Uri(res.Response.CheckoutUrl) : null,
                                     WebhookEndpointUrl = new Uri(res.Response.WebhookEndpointUrl),
                                     OnFailureRedirectUrl = new Uri(res.Response.OnFailureRedirectUrl),
                                     OnSuccessRedirectUrl = new Uri(res.Response.OnSuccessRedirectUrl),
                                     Customer = res.Customer,
                                     PaymentLink = res.PaymentLink,
                                     PaymentLinkId = res.Response.PaymentLinkId,
                                     CustomerId = res.Response.CustomerId,
                                     InvoiceId = res.Response.InvoiceId,
                                     Fees = res.Response.Fees,
                                     Status = Enum.GetValues<CheckoutStatus>().FirstOrDefault(x => x.ToString().Equals(res.Response.Status, StringComparison.OrdinalIgnoreCase)),
                                     Language = Language.GetLocalType(res.Response.Language) ?? LocaleType.English,
                                     Description = res.Response.Description,
                                     Items = res.Items,
                                     PaymentMethod = Enum.GetValues<PaymentMethod>().FirstOrDefault(x => x.ToString().Equals(res.Response.PaymentMethod, StringComparison.OrdinalIgnoreCase)),
                                     PassFeesToCustomer = res.Response.PassFeesToCustomer
                                   });

    CreateMap<(PagedApiResponse<CheckoutApiResponse> Response, List<CheckoutResponse> Items), Models.PagedResponse<CheckoutResponse>>()
     .ConstructUsing((res, ctx) => new Models.PagedResponse<CheckoutResponse>()
                                   {
                                     Data = res.Items,
                                     CurrentPage = res.Response.CurrentPage,
                                     FirstPage = res.Response.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.Response.GetTotalPages(),
                                     NextPage = res.Response.NextPageUrl?.GetPage(),
                                     PreviousPage = res.Response.PreviousPageUrl?.GetPage(),
                                     PageSize = res.Response.PageSize,
                                     Total = res.Response.Total
                                   });

    CreateMap<(CheckoutItemApiResponse Response, Models.Product? Product), CheckoutItem>()
     .ConstructUsing((res, ctx) => new CheckoutItem()
                                   {
                                     PriceId = res.Response.Id,
                                     Amount = res.Response.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Response.Currency),
                                     Metadata = res.Response.Metadata,
                                     Product = res.Product,
                                     ProductId = res.Response.ProductId,
                                     Quantity = res.Response.Quantity
                                   });
  }
}