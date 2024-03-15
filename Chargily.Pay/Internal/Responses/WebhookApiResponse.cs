namespace Chargily.Pay.Internal.Responses;

internal record WebhookApiResponse : BaseObjectApiResponse
{
  public CheckoutApiResponse Data { get; init; }
};