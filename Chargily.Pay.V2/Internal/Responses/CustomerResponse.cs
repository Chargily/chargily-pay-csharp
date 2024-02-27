using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Responses;

internal record CustomerResponse :  BaseObjectResponse
{
    public string Name { get; init; } = null!;
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressModel? Address { get; init; }
    public List<object>? Metadata { get; init; } = new();
}