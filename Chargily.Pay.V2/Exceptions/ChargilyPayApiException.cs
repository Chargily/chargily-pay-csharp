using Chargily.Pay.V2.Internal;

namespace Chargily.Pay.V2.Exceptions;

public class ChargilyPayApiException(int statusCode, string responseContent) : Exception
{
  public override string Message { get; } = new { StatusCode = statusCode, Response = responseContent }.Stringify();
}