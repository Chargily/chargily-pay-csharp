namespace Chargily.Pay.V2.Internal.Responses;

internal record WebhookApiResponse : BaseObjectApiResponse
{
  public CheckoutApiResponse Data { get; init; }
};