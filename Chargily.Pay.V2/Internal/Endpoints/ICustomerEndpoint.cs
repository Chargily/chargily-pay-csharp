using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyApi
{
    [Post("/customers")]
    Task<CustomerResponse> CreateCustomer([Body] CreateCustomerRequest request);

    [Post("/customers/{id}")]
    Task<CustomerResponse> UpdateCustomer([Query] string id, [Body] UpdateCustomerRequest request);

    [Get("/customers/{id}")]
    Task<CustomerResponse> GetCustomer([Query] string id);

    [Get("/customers")]
    Task<PagedResponse<CustomerResponse>> GetCustomers();
    
    [Delete("/customers/{id}")]
    Task<CustomerResponse?> DeleteCustomer([Query] string id);
}