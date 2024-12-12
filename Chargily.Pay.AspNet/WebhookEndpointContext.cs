using Chargily.Pay.Models;
using Microsoft.AspNetCore.Http;

namespace Chargily.Pay.AspNet;

public record WebhookEndpointContext
{
  public required bool SignatureIsValid { get; init; }
  public required WebhookRequest Request { get; init; }
  public required HttpContext HttpContext { get; init; }
}