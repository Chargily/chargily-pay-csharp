using Chargily.Pay.Abstractions;
using Chargily.Pay.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Chargily.Pay.AspNet;

public static class DependencyInjection
{
  public static IServiceCollection AddChargilyPayWebhookValidationMiddleware(this IServiceCollection services)
  {
    return services
          .AddSingleton<IWebhookValidator>(provider => provider.GetRequiredService<IChargilyPayClient>().WebhookValidator)
          .AddSingleton<ChargilyPayWebhookValidatorMiddleware>();
  }

  public static WebApplication UseChargilyPayWebhookValidation(this WebApplication application, string? overrideSignatureHeaderName = null)
  {
    ChargilyPayWebhookValidatorMiddleware.SignatureHeaderName = overrideSignatureHeaderName ?? "signature";
    application
     .UseMiddleware<ChargilyPayWebhookValidatorMiddleware>();
    return application;
  }

  public static RouteHandlerBuilder MapChargilyPayWebhookEndpoint(this WebApplication app, string endpointPath,
                                                                  Func<WebhookEndpointContext, Task> onWebhookReceived)
  {
    app.MapChargilyPayWebhookEndpoint("/chargily/webhook", async webhookContext =>
                                                           {
                                                             // you can access the webhook body, http context, validation result
                                                             if (webhookContext.SignatureIsValid)
                                                             {
                                                               var body = webhookContext.Request;
                                                               /* do something with the webhook request body */
                                                             }
                                                           });

    return app.MapMethods(endpointPath, [HttpMethods.Get, HttpMethods.Post],
                          async ([FromServices] IWebhookValidator validator, HttpContext context, [FromBody] WebhookRequest body) =>
                          {
                            var isValid = context.Items.ContainsKey(ChargilyPayWebhookValidatorMiddleware.IsValidHttpContextItemKey) ||
                                          validator.Validate(context.Request.Headers["Signature"].First()!, context.Request.Body);
                            await onWebhookReceived(new WebhookEndpointContext
                                                    {
                                                      SignatureIsValid = isValid,
                                                      HttpContext = context,
                                                      Request = body
                                                    });
                            return isValid ? Results.Ok(body.Data) : Results.Unauthorized();
                          });
  }

  /// <summary>
  ///   Map to an endpoint with the default path: `/chargily/webhook`
  /// </summary>
  /// <param name="app"></param>
  /// <param name="onWebhookReceived"></param>
  /// <returns></returns>
  public static RouteHandlerBuilder MapChargilyPayWebhookEndpoint(this WebApplication app, Func<WebhookEndpointContext, Task> onWebhookReceived)
  {
    return app.MapMethods("/chargily/webhook", [HttpMethods.Get, HttpMethods.Post],
                          async ([FromServices] IWebhookValidator validator, HttpContext context, [FromBody] WebhookRequest body) =>
                          {
                            var isValid = context.Items.ContainsKey(ChargilyPayWebhookValidatorMiddleware.IsValidHttpContextItemKey) ||
                                          validator.Validate(context.Request.Headers["Signature"].First()!, context.Request.Body);
                            await onWebhookReceived(new WebhookEndpointContext
                                                    {
                                                      SignatureIsValid = isValid,
                                                      HttpContext = context,
                                                      Request = body
                                                    });
                            return isValid ? Results.Ok(body.Data) : Results.Unauthorized();
                          });
  }
}