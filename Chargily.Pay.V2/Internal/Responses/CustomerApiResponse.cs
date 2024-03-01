using Chargily.Pay.V2.Internal.Requests;

namespace Chargily.Pay.V2.Internal.Responses;


internal record CustomerApiResponse :  BaseObjectApiResponse
{
    public string Name { get; init; } = null!;
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressRequest? Address { get; init; }
    public List<object>? Metadata { get; init; } = new();
}