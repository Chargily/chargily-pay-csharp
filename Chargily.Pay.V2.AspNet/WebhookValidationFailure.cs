using System.Net;

namespace Chargily.Pay.V2.AspNet;

internal sealed record WebhookValidationFailure(string ProvidedSignature)
{
  public string Message => "Signature Validation Failed!";
  public int StatusCode => (int)HttpStatusCode.BadRequest;
}