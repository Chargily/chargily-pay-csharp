using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyApi
{
    [Post("/checkouts")]
    Task<CheckoutApiApiResponse> CreateCheckout([Body] CreateCheckoutRequest request);

    [Post("/checkouts/{id}/expire")]
    Task<CheckoutApiApiResponse?> CancelCheckout([Query] string id);

    [Get("/checkouts/{id}")]
    Task<CheckoutApiApiResponse> GetCheckout([Query] string id);
    [Get("/checkouts/{id}/items")]
    Task<PagedApiResponse<ProductItemApiResponse>> GetCheckoutItems([Query] string id, [Query("page")] int page = 1, [Query("per_page")] int pageSize = 100);

    [Get("/checkouts")]
    Task<PagedApiResponse<CustomerApiResponse>> GetCheckouts([Query("page")] int page = 1, [Query("per_page")] int pageSize = 100);
    
}