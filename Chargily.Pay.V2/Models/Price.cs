namespace Chargily.Pay.V2.Models;

public sealed record Price : ProductPrice
{
  public string ProductId { get; init; }
  public Product? Product { get; internal init; }

  public CheckoutPriceItem ToCheckoutPrice(int quantity)
  {
    return new CheckoutPriceItem()
           {
             PriceId = Id,
             Quantity = quantity
           };
  }
}