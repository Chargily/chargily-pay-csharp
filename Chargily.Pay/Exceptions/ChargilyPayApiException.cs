using System.Net;
using Chargily.Pay.Internal;

namespace Chargily.Pay.Exceptions;

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