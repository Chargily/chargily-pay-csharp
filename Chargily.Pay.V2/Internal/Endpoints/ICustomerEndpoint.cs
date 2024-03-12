using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyPayApi
{
    [Post("/customers")]
    Task<CustomerApiResponse> CreateCustomer([Body] CreateCustomerRequest request);

    [Post("/customers/{id}")]
    Task<CustomerApiResponse> UpdateCustomer([Query] string id, [Body] UpdateCustomerRequest request);

    [Get("/customers/{id}")]
    Task<CustomerApiResponse> GetCustomer([Query] string id);

    [Get("/customers")]
    Task<PagedApiResponse<CustomerApiResponse>> GetCustomers([Query("page")] int page = 1, [Query("per_page")] int per_page = 50);
    
    [Delete("/customers/{id}")]
    Task<CustomerApiResponse?> DeleteCustomer([Query] string id);
}