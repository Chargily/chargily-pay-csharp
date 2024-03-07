using System.Net;
using System.Text.Json.Serialization;
using Chargily.Pay.V2.Internal;

namespace Chargily.Pay.V2.Exceptions;

public class ChargilyPayApiException(int statusCode, string responseContent) : Exception
{
  public override string Message { get; } = new
                                            {
                                              StatusCodeNumber = statusCode,
                                              StatusCode = (HttpStatusCode)statusCode,
                                              Response = responseContent
                                            }.Stringify();
}

public class ChargilyPayApiTooManyRequestsException(string message) : Exception
{
  public override string Message { get; } = new
                                            {
                                              StatusCodeNumber = (int)HttpStatusCode.TooManyRequests,
                                              StatusCode = HttpStatusCode.TooManyRequests,
                                              Response = message
                                            }.Stringify();
}