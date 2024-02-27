using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyApi
{
    [Post("/products")]
    Task<ProductResponse> CreateProduct([Body] CreateProductRequest request);

    [Post("/products/{id}")]
    Task<ProductResponse> UpdateProduct([Query] string id, [Body] UpdateProductRequest request);

    [Get("/products/{id}")]
    Task<ProductResponse> GetProduct([Query] string id);

    [Get("/products")]
    Task<PagedResponse<ProductResponse>> GetProducts();

    [Delete("/products/{id}")]
    Task<ProductResponse?> DeleteProduct([Query] string id);

    [Get("/products/{id}/prices")]
    Task<PagedResponse<PriceResponse>> GetProductPrices([Query] string id);
}