namespace Chargily.Pay.Models;

public record CheckoutPriceItem
{
  public string PriceId { get; init; }
  public int Quantity { get; init; }
}