using AutoMapper;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<CreateProduct, CreateProductRequest>()
     .ConstructUsing((req, ctx) => new CreateProductRequest()
                                   {
                                     ImagesUrls = req.ImagesUrls
                                                     .Select(x => x.ToString())
                                                     .ToArray(),
                                     Description = req.Description,
                                     Metadata = req.Metadata,
                                     Name = req.Name,
                                   });

    CreateMap<UpdateProduct, UpdateProductRequest>()
     .ConstructUsing((req, ctx) => new UpdateProductRequest()
                                   {
                                     Id = req.Id,
                                     ImagesUrls = req.ImagesUrls?
                                                     .Select(x => x.ToString())
                                                     .ToArray(),
                                     Description = req.Description,
                                     Metadata = req.Metadata,
                                     Name = req.Name,
                                   });

    CreateMap<ProductApiResponse, Response<Product>>()
     .ConstructUsing((res, ctx) => new Response<Product>()
                                   {
                                     Id = res.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.LastUpdatedAt),
                                     IsLiveMode = res.IsLiveMode,
                                     Value = new Product()
                                             {
                                               Id = res.Id,
                                               Description = res.Description,
                                               ImagesUrls = res.ImagesUrls?
                                                               .Select(x => new Uri(x, UriKind.Absolute))
                                                               .ToList(),
                                               Metadata = res.Metadata,
                                               Name = res.Name,
                                               Prices = []
                                             }
                                   });

    CreateMap<ProductApiResponse, Product>()
     .ConstructUsing((res, ctx) => new Product()
                                   {
                                     Id = res.Id,
                                     Description = res.Description,
                                     ImagesUrls = res.ImagesUrls?
                                                     .Select(x => new Uri(x, UriKind.Absolute))
                                                     .ToList(),
                                     Metadata = res.Metadata,
                                     Name = res.Name,
                                     Prices = []
                                   });

    CreateMap<(ProductApiResponse Response, List<ProductPrice> Items), Response<Product>>()
     .ConstructUsing((res, ctx) => new Response<Product>()
                                   {
                                     Id = res.Response.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Response.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.Response.LastUpdatedAt),
                                     IsLiveMode = res.Response.IsLiveMode,
                                     Value = new Product()
                                             {
                                               Id = res.Response.Id,
                                               Description = res.Response.Description,
                                               ImagesUrls = res.Response.ImagesUrls?
                                                               .Select(x => new Uri(x, UriKind.Absolute))
                                                               .ToList(),
                                               Metadata = res.Response.Metadata,
                                               Name = res.Response.Name,
                                               Prices = res.Items
                                             }
                                   });

    CreateMap<(PagedApiResponse<ProductApiResponse> Response, List<Product> Items), PagedResponse<Product>>()
     .ConstructUsing((res, ctx) => new PagedResponse<Product>()
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
    CreateMap<PriceApiResponse, ProductPrice>()
     .ConstructUsing((res, ctx) => new ProductPrice()
                                   {
                                     Id = res.Id,
                                     Metadata = res.Metadata,
                                     Amount = res.Amount,
                                     Currency = ctx.Mapper.Map<Currency>(res.Currency)
                                   });
  }
}