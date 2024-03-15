namespace Chargily.Pay.Models;

public sealed record PaymentLinkItem
{
    public string ProductId { get; init; }
    public Product? Product { get; init; }
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    public int Quantity { get; init; }
    public bool AdjustableQuantity { get; init; }
    public List<string>? Metadata { get; init; } = new();
}