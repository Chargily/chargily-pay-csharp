namespace Chargily.Pay.V2.Models;

public record PaymentLink
{
  public string Id { get; set; }
  public string Name { get; init; }
  public bool IsActive { get; init; }
  public string CompletionMessage { get; init; }
  public LocaleType Language { get; init; }
  public bool PassFeesToCustomer { get; init; }
  public List<string>? Metadata { get; init; } = new();
  
  // public string? ShippingAddress { get; init; }
  public bool CollectShippingAddress { get; init; }
}