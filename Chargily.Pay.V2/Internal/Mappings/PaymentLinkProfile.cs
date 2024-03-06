using AutoMapper;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class PaymentLinkProfile : Profile
{
  public PaymentLinkProfile()
  {

    CreateMap<CreatePaymentLink, CreatePaymentLinkRequest>()
     .ConstructUsing((req, ctx) => new CreatePaymentLinkRequest()
                                   {
                                     Metadata = req.Metadata,
                                     PassFeesToCustomer = req.PassFeesToCustomer,
                                     Language = Language.GetLanguage(req.Language) ?? "en",
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
                                     Language = Language.GetLanguage(req.Language ?? LocaleType.English),
                                     CompletionMessage = req.CompletionMessage,
                                     IsActive = req.IsActive,
                                     Name = req.Name
                                   });

    CreateMap<(PaymentLinkApiResponse Response, List<PaymentLinkItem> Items), Response<PaymentLinkResponse>>()
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
                                               Url = new Uri(res.Response.Url),
                                             },
                                   });
    CreateMap<(PaymentLinkApiResponse Response, List<PaymentLinkItem> Items), PaymentLinkResponse>()
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
                                     Url = new Uri(res.Response.Url),
                                   });

    CreateMap<(PagedApiResponse<PaymentLinkApiResponse> Response, List<PaymentLinkResponse> Items), PagedResponse<PaymentLinkResponse>>()
     .ConstructUsing((res, ctx) => new PagedResponse<PaymentLinkResponse>()
                                   {
                                     Data = res.Items,
                                     CurrentPage = res.Response.CurrentPage,
                                     FirstPage = res.Response.FirstPageUrl.GetPageOrDefault(1),
                                     //LastPage = res.Response.LastPageUrl.GetPageOrDefault(1),
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