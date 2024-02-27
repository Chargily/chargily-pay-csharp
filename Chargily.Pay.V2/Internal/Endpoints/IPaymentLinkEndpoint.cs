using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyApi
{
    [Post("/payment-links")]
    Task<PaymentLinkResponse> CreatePaymentLink([Body] CreatePaymentLinkRequest request);

    [Post("/payment-links/{id}")]
    Task<PaymentLinkResponse?> UpdatePaymentLink([Query] string id,[Body] UpdatePaymentLinkRequest request);

    [Get("/payment-links/{id}")]
    Task<PaymentLinkResponse> GetPaymentLink([Query] string id);
    [Get("/payment-links/{id}/items")]
    Task<PagedResponse<ProductItemResponse>> GetPaymentLinkItems([Query] string id);

    [Get("/payment-links")]
    Task<PagedResponse<PaymentLinkResponse>> GetPaymentLinks();
    
}