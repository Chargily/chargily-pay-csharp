using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyApi
{
    [Post("/checkouts")]
    Task<CheckoutResponse> CreateCheckout([Body] CreateCheckoutRequest request);

    [Post("/checkouts/{id}/expire")]
    Task<CheckoutResponse?> CancelCheckout([Query] string id);

    [Get("/checkouts/{id}")]
    Task<CheckoutResponse> GetCheckout([Query] string id);
    [Get("/checkouts/{id}/items")]
    Task<PagedResponse<ProductItemResponse>> GetCheckoutItems([Query] string id);

    [Get("/checkouts")]
    Task<PagedResponse<CustomerResponse>> GetCheckouts();
    
}