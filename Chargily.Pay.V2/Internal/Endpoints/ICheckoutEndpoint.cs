using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyPayApi
{
    [Post("/checkouts")]
    Task<CheckoutApiResponse> CreateCheckout([Body] CreateCheckoutRequest request);

    [Post("/checkouts/{id}/expire")]
    Task<CheckoutApiResponse?> CancelCheckout([Query] string id);

    [Get("/checkouts/{id}")]
    Task<CheckoutApiResponse> GetCheckout([Query] string id);
    [Get("/checkouts/{id}/items")]
    Task<PagedApiResponse<CheckoutItemApiResponse>> GetCheckoutItems([Query] string id, [Query("page")] int page = 1, [Query("per_page")] int pageSize = 50);

    [Get("/checkouts")]
    Task<PagedApiResponse<CheckoutApiResponse>> GetCheckouts([Query("page")] int page = 1, [Query("per_page")] int pageSize = 50);
    
}