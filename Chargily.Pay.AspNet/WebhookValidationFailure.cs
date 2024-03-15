using System.Net;

namespace Chargily.Pay.AspNet;

internal sealed record WebhookValidationFailure(string ProvidedSignature)
{
  public string Message => "Signature Validation Failed!";
  public int StatusCode => (int)HttpStatusCode.BadRequest;
}