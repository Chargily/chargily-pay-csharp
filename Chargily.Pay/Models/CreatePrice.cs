namespace Chargily.Pay.Models;

public sealed record CreatePrice
{
  public decimal Amount { get; init; }
  public Currency Currency { get; init; }
  public List<string>? Metadata { get; init; }
  public string ProductId { get; init; }
}