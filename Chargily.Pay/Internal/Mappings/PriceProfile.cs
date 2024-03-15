using AutoMapper;
using Chargily.Pay.Internal.Requests;
using Chargily.Pay.Internal.Responses;
using Chargily.Pay.Models;
using CreatePrice = Chargily.Pay.Models.CreatePrice;
using Currency = Chargily.Pay.Models.Currency;
using Price = Chargily.Pay.Models.Price;
using UpdatePrice = Chargily.Pay.Models.UpdatePrice;

namespace Chargily.Pay.Internal.Mappings;

public class PriceProfile : Profile
{
  public PriceProfile()
  {
    CreateMap<CreatePrice, CreatePriceRequest>()
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

    CreateMap<PriceApiResponse, Models.Response<Models.Price>>()
     .ConstructUsing((res, ctx) => new Models.Response<Models.Price>()
                                   {
                                     Id = res.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.LastUpdatedAt),
                                     IsLiveMode = res.IsLiveMode,
                                     Value = new Models.Price()
                                             {
                                               Id = res.Id,
                                               Metadata = res.Metadata,
                                               Amount = res.Amount,
                                               Currency = ctx.Mapper.Map<Currency>(res.Currency),
                                               ProductId = res.ProductId
                                             }
                                   });

    CreateMap<PriceApiResponse, Models.Price>()
     .ConstructUsing((res, ctx) => new Models.Price()
                                   {
                                     Id = res.Id,
                                     Metadata = res.Metadata,
                                     Amount = res.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Currency),
                                     ProductId = res.ProductId
                                   });
    
    CreateMap< (PriceApiResponse Price, Models.Product Product), Models.Price>()
     .ConstructUsing((res, ctx) => new Models.Price()
                                   {
                                     Id = res.Price.Id,
                                     Metadata = res.Price.Metadata,
                                     Amount = res.Price.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Price.Currency),
                                     ProductId = res.Price.ProductId,
                                     Product = res.Product
                                   });
    
    CreateMap< (PriceApiResponse Price, Models.Response<Models.Product> Product), Response<Price>>()
     .ConstructUsing((res, ctx) => new Response<Price>()
                                   {
                                     Id = res.Price.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Price.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Price.LastUpdatedAt),
                                     IsLiveMode = res.Price.IsLiveMode,
                                     Value = new Price()
                                             {
                                               Metadata = res.Price.Metadata,
                                               Amount = res.Price.Amount,
                                               Currency = ctx.Mapper.Map<Currency>(res.Price.Currency),
                                               ProductId = res.Price.ProductId,
                                               Product = res.Product.Value
                                             }
                                   });
    CreateMap<PagedApiResponse<PriceApiResponse>, PagedResponse<Price>>()
     .ConstructUsing((res, ctx) => new PagedResponse<Price>()
                                   {
                                     Data = ctx.Mapper.Map<List<Price>>(res.Data),
                                     CurrentPage = res.CurrentPage,
                                     FirstPage = res.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.GetTotalPages(),
                                     NextPage = res.NextPageUrl?.GetPage(),
                                     PreviousPage = res.PreviousPageUrl?.GetPage(),
                                     PageSize = res.PageSize,
                                     Total = res.Total
                                   });
    
    CreateMap<(PagedApiResponse<PriceApiResponse> Response, List<Price> Prices), PagedResponse<Price>>()
     .ConstructUsing((res, ctx) => new PagedResponse<Price>()
                                   {
                                     Data = res.Prices,
                                     CurrentPage = res.Response.CurrentPage,
                                     FirstPage = res.Response.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.Response.GetTotalPages(),
                                     NextPage = res.Response.NextPageUrl?.GetPage(),
                                     PreviousPage = res.Response.PreviousPageUrl?.GetPage(),
                                     PageSize = res.Response.PageSize,
                                     Total = res.Response.Total
                                   });
  }
}