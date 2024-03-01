namespace Chargily.Pay.V2.Models;


public record ProductPrice
{
    public string Id { get; init; }
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    public List<object>? Metadata { get; init; }
}
public sealed record Price : ProductPrice
{
    public string ProductId { get; init; }
    public Product? Product { get; internal init; }
}