namespace Chargily.Pay.V2.Models;

public sealed record CreateCheckoutItem
{
  public int Quantity { get; init; }
  public string ProductId { get; init; }
  public Currency Currency { get; init; }
  public List<string>? Metadata { get; init; } = new();
  public decimal Amount { get; init; }
}