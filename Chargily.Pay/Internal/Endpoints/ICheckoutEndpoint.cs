using Chargily.Pay.Internal.Requests;
using Chargily.Pay.Internal.Responses;
using Refit;

namespace Chargily.Pay.Internal.Endpoints;

internal partial interface IChargilyPayApi
{
    [Post("/checkouts")]
    Task<CheckoutApiResponse> CreateCheckout([Body] CreateCheckoutRequest request);

    [Post("/checkouts/{id}/expire")]
    Task<CheckoutApiResponse?> CancelCheckout([Query] string id);

    [Get("/checkouts/{id}")]
    Task<CheckoutApiResponse> GetCheckout([Query] string id);
    [Get("/checkouts/{id}/items")]
    Task<PagedApiResponse<CheckoutItemApiResponse>> GetCheckoutItems([Query] string id, [Query("page")] int page = 1, [Query("per_page")] int per_page = 50);

    [Get("/checkouts")]
    Task<PagedApiResponse<CheckoutApiResponse>> GetCheckouts([Query("page")] int page = 1, [Query("per_page")] int per_page = 50);
    
}