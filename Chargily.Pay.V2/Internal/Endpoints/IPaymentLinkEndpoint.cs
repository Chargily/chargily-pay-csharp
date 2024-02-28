using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyApi
{
    [Post("/payment-links")]
    Task<PaymentLinkApiApiResponse> CreatePaymentLink([Body] CreatePaymentLinkRequest request);

    [Post("/payment-links/{id}")]
    Task<PaymentLinkApiApiResponse?> UpdatePaymentLink([Query] string id,[Body] UpdatePaymentLinkRequest request);

    [Get("/payment-links/{id}")]
    Task<PaymentLinkApiApiResponse> GetPaymentLink([Query] string id);
    [Get("/payment-links/{id}/items")]
    Task<PagedApiResponse<ProductItemApiResponse>> GetPaymentLinkItems([Query] string id,[Query("page")] int page = 1, [Query("per_page")] int pageSize = 100);

    [Get("/payment-links")]
    Task<PagedApiResponse<PaymentLinkApiApiResponse>> GetPaymentLinks([Query("page")] int page = 1, [Query("per_page")] int pageSize = 100);
    
}