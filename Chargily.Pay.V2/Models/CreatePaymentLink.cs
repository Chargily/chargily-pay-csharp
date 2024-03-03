namespace Chargily.Pay.V2.Models;

public record CreatePaymentLink
{
  public string Name { get; init; }
  public bool IsActive { get; init; }
  public string CompletionMessage { get; init; }
  public LocaleType Language { get; init; }
  public bool PassFeesToCustomer { get; init; }
  public List<string>? Metadata { get; init; } = new();
}