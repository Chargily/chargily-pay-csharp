namespace Chargily.Pay.V2.Models;

public sealed record CheckoutPriceItem
{
  public string PriceId { get; init; }
  public int Quantity { get; init; }
}