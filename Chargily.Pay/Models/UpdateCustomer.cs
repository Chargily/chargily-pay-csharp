namespace Chargily.Pay.Models;

public sealed record UpdateCustomer
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public CustomerAddress? Address { get; init; }
    public List<string>? Metadata { get; init; } = new();
}