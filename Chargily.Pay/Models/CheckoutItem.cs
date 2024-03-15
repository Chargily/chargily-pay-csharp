namespace Chargily.Pay.Models;

public sealed record CheckoutItem
{
    public string PriceId { get; init; }
    public int Quantity { get; init; }
    public Product? Product { get; internal set; }
    public string ProductId { get; init; }
    public Currency Currency { get; init; }
    public List<string>? Metadata { get; init; } = new();
    public decimal Amount { get; init; }

}