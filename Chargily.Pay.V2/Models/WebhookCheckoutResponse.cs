namespace Chargily.Pay.V2.Models;

public sealed record WebhookCheckoutResponse
{
  public string Id { get; init; }

  public decimal? Amount { get; internal set; }
  public Currency? Currency { get; internal set; }
  public string? CustomerId { get; init; }
  public PaymentMethod PaymentMethod { get; init; }
  public Uri? OnSuccessRedirectUrl { get; init; }
  public Uri? OnFailureRedirectUrl { get; init; }
  public Uri? WebhookEndpointUrl { get; init; }
  public string? Description { get; init; }
  public LocaleType Language { get; init; }
  public bool PassFeesToCustomer { get; init; }
  public List<string>? Metadata { get; init; } = new();

  public Uri CheckoutUrl { get; init; }
  public string InvoiceId { get; init; }
  public string? PaymentLinkId { get; init; }
  public decimal Fees { get; init; }
  public CheckoutStatus Status { get; init; }
}