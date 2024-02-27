namespace Chargily.Pay.V2.Models;

public sealed record CheckoutItem
{
    public Price Price { get; init; }
    public int Quantity { get; init; }
}