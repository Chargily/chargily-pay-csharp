using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyPayApi
{
    [Post("/products")]
    Task<ProductApiResponse> CreateProduct([Body] CreateProductRequest request);

    [Post("/products/{id}")]
    Task<ProductApiResponse> UpdateProduct([Query] string id, [Body] UpdateProductRequest request);

    [Get("/products/{id}")]
    Task<ProductApiResponse> GetProduct([Query("id")] string id);

    [Get("/products")]
    Task<PagedApiResponse<ProductApiResponse>> GetProducts([Query("page")] int page = 1, [Query("per_page")] int pageSize = 50);

    [Delete("/products/{id}")]
    Task<ProductApiResponse?> DeleteProduct([Query] string id);

    [Get("/products/{id}/prices")]
    Task<PagedApiResponse<PriceApiResponse>> GetProductPrices([Query] string id, [Query("page")] int page = 1, [Query("per_page")] int pageSize = 50);
}