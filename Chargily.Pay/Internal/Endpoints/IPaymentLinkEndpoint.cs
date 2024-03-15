using Chargily.Pay.Internal.Requests;
using Chargily.Pay.Internal.Responses;
using Refit;

namespace Chargily.Pay.Internal.Endpoints;

internal partial interface IChargilyPayApi
{
    [Post("/payment-links")]
    Task<PaymentLinkApiResponse> CreatePaymentLink([Body] CreatePaymentLinkRequest request);

    [Post("/payment-links/{id}")]
    Task<PaymentLinkApiResponse?> UpdatePaymentLink([Query] string id,[Body] UpdatePaymentLinkRequest request);

    [Get("/payment-links/{id}")]
    Task<PaymentLinkApiResponse> GetPaymentLink([Query] string id);
    [Get("/payment-links/{id}/items")]
    Task<PagedApiResponse<PaymentLinkItemApiResponse>> GetPaymentLinkItems([Query] string id,[Query("page")] int page = 1, [Query("per_page")] int per_page = 50);

    [Get("/payment-links")]
    Task<PagedApiResponse<PaymentLinkApiResponse>> GetPaymentLinks([Query("page")] int page = 1, [Query("per_page")] int per_page = 50);
    
}