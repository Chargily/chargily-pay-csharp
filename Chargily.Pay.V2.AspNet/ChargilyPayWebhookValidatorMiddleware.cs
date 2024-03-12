using System.Text.Json;
using Chargily.Pay.V2.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Chargily.Pay.V2.AspNet;

internal sealed class ChargilyPayWebhookValidatorMiddleware(IWebhookValidator validator, ILogger<ChargilyPayWebhookValidatorMiddleware> logger)
  : IMiddleware
{
  public const string IsValidHttpContextItemKey = "chargily_webhook_validated";
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      if (context.Request.Headers.TryGetValue("Signature", out var value)
          && HttpMethods.IsPost(context.Request.Method))
      {
        var signature = value.First()!;
        logger.LogInformation("Intercepted a request with 'Signature' Header: {@signature}", signature);

        var isValid = validator.Validate(signature, context.Request.Body);
        switch (isValid)
        {
          case true:
            context.Items.Add(IsValidHttpContextItemKey, true);
            break;
          case false:
          {
            var response = new WebhookValidationFailure(signature);
            logger.LogError("Signature Validation Failed! Request with 'Signature' Header: {@signature}", signature);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            break;
          }
        }
      }
    }
    catch (Exception ex)
    {
      logger.LogError(ex.Message);
    }
    finally
    {
      await next(context);
    }
  }
}

