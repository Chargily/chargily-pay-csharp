using Chargily.Pay.Internal.Requests;

namespace Chargily.Pay.Internal.Responses;


internal record CustomerApiResponse :  BaseObjectApiResponse
{
    public string Name { get; init; } = null!;
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressRequest? Address { get; init; }
    public List<string>? Metadata { get; init; } = new();
}