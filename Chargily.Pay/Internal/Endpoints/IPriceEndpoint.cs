using Chargily.Pay.Internal.Requests;
using Chargily.Pay.Internal.Responses;
using Refit;

namespace Chargily.Pay.Internal.Endpoints;

internal partial interface IChargilyPayApi
{
    [Post("/prices")]
    Task<PriceApiResponse> CreatePrice([Body] CreatePriceRequest request);

    [Post("/prices/{id}")]
    Task<PriceApiResponse> UpdatePrice([Query] string id, [Body] UpdatePriceRequest request);

    [Get("/prices/{id}")]
    Task<PriceApiResponse> GetPrice([Query] string id);

    [Get("/prices")]
    Task<PagedApiResponse<PriceApiResponse>> GetPrices([Query("page")] int page = 1, [Query("per_page")] int per_page = 50);
    
}