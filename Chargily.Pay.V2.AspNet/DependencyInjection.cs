using Chargily.Pay.V2.Abstractions;
using Chargily.Pay.V2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Chargily.Pay.V2.AspNet;

public static class DependencyInjection
{
  public static IServiceCollection AddChargilyPayWebhookValidationMiddleware(this IServiceCollection services)
  {
    return services.AddSingleton<ChargilyPayWebhookValidatorMiddleware>();
  }

  public static WebApplication UseChargilyPayWebhookValidation(this WebApplication application)
  {
     application.UseMiddleware<ChargilyPayWebhookValidatorMiddleware>();
     return application;
  }

  public static RouteHandlerBuilder MapChargilyPayWebhookEndpoint(this WebApplication app, string endpointPath = "/chargily/webhook")
  {
    return app.MapPost(endpointPath,
                       ([FromServices] IWebhookValidator validator, HttpContext context, [FromBody] WebhookRequest body) =>
                       {
                         var isValid = context.Items.ContainsKey(ChargilyPayWebhookValidatorMiddleware.IsValidHttpContextItemKey) ||
                           validator.Validate(context.Request.Headers["Signature"].First()!, context.Request.Body);
                         return isValid ? Results.Ok(body.Data) : Results.Unauthorized();
                       });
  }
}