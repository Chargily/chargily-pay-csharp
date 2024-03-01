namespace Chargily.Pay.V2.Models;

public sealed record PaymentLinkItem
{
    public string ProductId { get; init; }
    public Product? Product { get; init; }
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    public int Quantity { get; init; }
    public bool AdjustableQuantity { get; init; }
    public List<object>? Metadata { get; init; } = new();
}