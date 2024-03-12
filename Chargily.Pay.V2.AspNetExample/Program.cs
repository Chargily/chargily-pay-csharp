using Chargily.Pay.V2;
using Chargily.Pay.V2.AspNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services
        // Register Chargily Pay Client
       .AddGlobalChargilyPayClient(config =>
                                   {
                                     // toggle live mode
                                     config.IsLiveMode = false;
                                     // your chargily dev account api-secret key
                                     config.ApiSecretKey = "YOUR API SECRET";
                                   })
        // Register Chargily Pay Webhook Signature Validator
       .AddChargilyPayWebhookValidationMiddleware();

var app = builder.Build();

// User Chargily Pay Webhook Signature Validator Middleware
app.UseChargilyPayWebhookValidation();

// Map Webhook Endpoint to `both POST & GET /api/checkout-webhook`
app.MapChargilyPayWebhookEndpoint(endpointPath: "/api/checkout-webhook");

app.Run();
