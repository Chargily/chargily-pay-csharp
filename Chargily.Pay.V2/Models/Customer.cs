namespace Chargily.Pay.V2.Models;

public sealed record Customer
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public CustomerAddress? Address { get; init; }
    public List<object>? Metadata { get; init; } = new();
}