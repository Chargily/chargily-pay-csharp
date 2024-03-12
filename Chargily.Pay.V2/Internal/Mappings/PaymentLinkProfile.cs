using AutoMapper;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class PaymentLinkProfile : Profile
{
  public PaymentLinkProfile()
  {

    CreateMap<PaymentLinkPriceItem, PaymentLinkItemPriceRequest>()
     .ConstructUsing((req, ctx) => new PaymentLinkItemPriceRequest()
                                   {
                                     Quantity = req.Quantity,
                                     PriceId = req.PriceId,
                                     AdjustableQuantity = req.AdjustableQuantity
                                   });
    
    CreateMap<CreatePaymentLink, CreatePaymentLinkRequest>()
     .ConstructUsing((req, ctx) => new CreatePaymentLinkRequest()
                                   {
                                     Metadata = req.Metadata,
                                     PassFeesToCustomer = req.PassFeesToCustomer,
                                     Language = Language.GetLanguage(req.Language) ?? "en",
                                     CompletionMessage = req.CompletionMessage,
                                     IsActive = req.IsActive,
                                     Name = req.Name,
                                     Items = ctx.Mapper.Map<List<PaymentLinkItemPriceRequest>>(req.Items),
                                     CollectShippingAddress = req.CollectShippingAddress,
                                     // ShippingAddress = req.ShippingAddress
                                   });
    CreateMap<UpdatePaymentLink, UpdatePaymentLinkRequest>()
     .ConstructUsing((req, ctx) => new UpdatePaymentLinkRequest()
                                   {
                                     Id = req.Id,
                                     Metadata = req.Metadata,
                                     PassFeesToCustomer = req.PassFeesToCustomer,
                                     Language = Language.GetLanguage(req.Language ?? LocaleType.English),
                                     CompletionMessage = req.CompletionMessage,
                                     IsActive = req.IsActive,
                                     Name = req.Name,
                                     CollectShippingAddress = req.CollectShippingAddress,
                                     // ShippingAddress = req.ShippingAddress,
                                     Items = ctx.Mapper.Map<List<PaymentLinkItemPriceRequest>>(req.Items),
                                   });

    CreateMap<(PaymentLinkApiResponse Response, List<PaymentLinkItem>? Items), Response<PaymentLinkResponse>>()
     .ConstructUsing((res, ctx) => new Response<PaymentLinkResponse>()
                                   {
                                     Id = res.Response.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Response.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Response.LastUpdatedAt),
                                     IsLiveMode = res.Response.IsLiveMode,
                                     Value = new PaymentLinkResponse()
                                             {
                                               Id = res.Response.Id,
                                               Metadata = res.Response.Metadata,
                                               Language = Language.GetLocalType(res.Response.Language) ?? LocaleType.English ,
                                               Items = res.Items,
                                               PassFeesToCustomer = res.Response.PassFeesToCustomer,
                                               CompletionMessage = res.Response.CompletionMessage,
                                               IsActive = res.Response.IsActive,
                                               Name = res.Response.Name,
                                               Url = Uri.TryCreate(res.Response.Url, UriKind.Absolute, out var uri) ? uri: null ,
                                               // ShippingAddress = res.Response.ShippingAddress,
                                               CollectShippingAddress = res.Response.CollectShippingAddress
                                             },
                                   });
    CreateMap<(PaymentLinkApiResponse Response, List<PaymentLinkItem>? Items), PaymentLinkResponse>()
     .ConstructUsing((res, ctx) => new PaymentLinkResponse()
                                   {
                                     Id = res.Response.Id,
                                     Metadata = res.Response.Metadata,
                                     Language = Language.GetLocalType(res.Response.Language) ?? LocaleType.English,
                                     Items = res.Items,
                                     PassFeesToCustomer = res.Response.PassFeesToCustomer,
                                     CompletionMessage = res.Response.CompletionMessage,
                                     IsActive = res.Response.IsActive,
                                     Name = res.Response.Name,
                                     Url = Uri.TryCreate(res.Response.Url, UriKind.Absolute, out var uri) ? uri: null ,
                                     // ShippingAddress = res.Response.ShippingAddress,
                                     CollectShippingAddress = res.Response.CollectShippingAddress,
                                     
                                   });

    CreateMap<(PagedApiResponse<PaymentLinkApiResponse> Response, List<PaymentLinkResponse> Items), PagedResponse<PaymentLinkResponse>>()
     .ConstructUsing((res, ctx) => new PagedResponse<PaymentLinkResponse>()
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

    CreateMap<(PaymentLinkItemApiResponse? Response, Product? Product), PaymentLinkItem>()
     .ConstructUsing((res, ctx) => new PaymentLinkItem()
                                 {
                                   Metadata = res.Response.Metadata,
                                   Amount = res.Response.Amount,
                                   Currency = ctx.Mapper.Map<Currency>(res.Response.Currency),
                                   Product = res.Product,
                                   Quantity = res.Response.Quantity,
                                   ProductId = res.Response.ProductId,
                                   AdjustableQuantity = res.Response.AdjustableQuantity,
                                 });
  }
}