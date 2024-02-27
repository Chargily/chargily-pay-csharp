using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyApi
{
    [Post("/prices")]
    Task<PriceResponse> CreatePrice([Body] CreatePriceRequest request);

    [Post("/prices/{id}")]
    Task<PriceResponse> UpdatePrice([Query] string id, [Body] UpdatePriceRequest request);

    [Get("/prices/{id}")]
    Task<PriceResponse> GetPrice([Query] string id);

    [Get("/prices")]
    Task<PagedResponse<PriceResponse>> GetPrices();
    
}