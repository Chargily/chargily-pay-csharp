using System.Text.Json.Serialization;
using Chargily.Pay.V2.Internal.Requests;

namespace Chargily.Pay.V2.Models;

public sealed record PaymentLinkPriceItem:  CheckoutPriceItem
{
  public bool AdjustableQuantity { get; init; }
}

public sealed record CreatePaymentLink
{
  public CreatePaymentLink(List<PaymentLinkPriceItem> items)
  {
    Items = items;
  }
  public string Name { get; init; }
  public bool IsActive { get; init; }
  public string CompletionMessage { get; init; }
  public LocaleType Language { get; init; }
  public bool PassFeesToCustomer { get; init; }
  public List<string>? Metadata { get; init; } = new();
  
  public IReadOnlyList<PaymentLinkPriceItem> Items { get; internal set; }
  
  // public string? ShippingAddress { get; init; }
  public bool CollectShippingAddress { get; init; }
}