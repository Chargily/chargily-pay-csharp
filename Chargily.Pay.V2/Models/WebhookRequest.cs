namespace Chargily.Pay.V2.Models;

public sealed record WebhookRequest
{
  public string Id { get; init; }
  public bool IsLiveMode { get; init; }
  public DateTimeOffset CreatedAt { get; init; }
  public DateTimeOffset LastUpdatedAt { get; init; }
  public WebhookCheckoutResponse Data { get; init; }
}