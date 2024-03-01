using AutoMapper;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class PriceProfile : Profile
{
  public PriceProfile()
  {
    CreateMap<Price, CreatePriceRequest>()
     .ConstructUsing((req, ctx) => new CreatePriceRequest()
                                   {
                                     Metadata = req.Metadata,
                                     ProductId = req.ProductId,
                                     Amount = req.Amount,
                                     Currency = ctx.Mapper.Map<string>(req.Currency)
                                   });

    CreateMap<UpdatePrice, UpdatePriceRequest>()
     .ConstructUsing((req, ctx) => new UpdatePriceRequest()
                                   {
                                     Id = req.Id,
                                     Metadata = req.Metadata,
                                     ProductId = req.ProductId,
                                     Amount = req.Amount,
                                     Currency = ctx.Mapper.Map<string>(req.Currency)
                                   });

    CreateMap<PriceApiResponse, Response<Price>>()
     .ConstructUsing((res, ctx) => new Response<Price>()
                                   {
                                     Id = res.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(res.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeMilliseconds(res.LastUpdatedAt),
                                     IsLiveMode = res.IsLiveMode,
                                     Value = new Price()
                                             {
                                               Id = res.Id,
                                               Metadata = res.Metadata,
                                               Amount = res.Amount,
                                               Currency = ctx.Mapper.Map<Currency>(res.Currency),
                                               ProductId = res.ProductId
                                             }
                                   });

    CreateMap<PriceApiResponse, Price>()
     .ConstructUsing((res, ctx) => new Price()
                                   {
                                     Id = res.Id,
                                     Metadata = res.Metadata,
                                     Amount = res.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Currency),
                                     ProductId = res.ProductId
                                   });
    
    CreateMap< (PriceApiResponse Price, Product Product), Price>()
     .ConstructUsing((res, ctx) => new Price()
                                   {
                                     Id = res.Price.Id,
                                     Metadata = res.Price.Metadata,
                                     Amount = res.Price.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Price.Currency),
                                     ProductId = res.Price.ProductId,
                                     Product = res.Product
                                   });
    CreateMap<PagedApiResponse<PriceApiResponse>, PagedResponse<Price>>()
     .ConstructUsing((res, ctx) => new PagedResponse<Price>()
                                   {
                                     Data = ctx.Mapper.Map<List<Price>>(res.Data),
                                     CurrentPage = res.CurrentPage,
                                     FirstPage = res.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.LastPageUrl.GetPageOrDefault(1),
                                     NextPage = res.NextPageUrl?.GetPage(),
                                     PreviousPage = res.NextPageUrl?.GetPage(),
                                     PageSize = res.PageSize,
                                     Total = res.Total
                                   });
    
    CreateMap<(PagedApiResponse<PriceApiResponse> Response, List<Price> Prices), PagedResponse<Price>>()
     .ConstructUsing((res, ctx) => new PagedResponse<Price>()
                                   {
                                     Data = res.Prices,
                                     CurrentPage = res.Response.CurrentPage,
                                     FirstPage = res.Response.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.Response.LastPageUrl.GetPageOrDefault(1),
                                     NextPage = res.Response.NextPageUrl?.GetPage(),
                                     PreviousPage = res.Response.NextPageUrl?.GetPage(),
                                     PageSize = res.Response.PageSize,
                                     Total = res.Response.Total
                                   });
  }
}