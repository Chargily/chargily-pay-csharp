# Welcome to C# Package Repository
# for [Chargily Pay](https://chargily.com/business/pay "Chargily Pay")™ Gateway - V2.

Thank you for your interest in C# Package of Chargily Pay™, an open source project by Chargily, a leading fintech company in Algeria specializing in payment solutions and  e-commerce facilitating, this Package is providing the easiest and free way to integrate e-payment API through widespread payment methods in Algeria such as EDAHABIA (Algerie Post) and CIB (SATIM) into your C#/ASP.NET projects.

This package is developed by **Ahmed Chakhoum ([rainxh11](https://github.com/rainxh11))** and is open to contributions from developers like you.

| Nuget Pacakge                                                                                                                                                | Downloads                                                               |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------|
| Chargily.Pay: <br/>[![Latest version](https://img.shields.io/nuget/v/Chargily.Pay.svg)](https://www.nuget.org/packages/Chargily.Pay/)                     | ![Downloads](https://img.shields.io/nuget/dt/Chargily.Pay.svg)        |
| Chargily.Pay.AspNet:<br/> [![Latest version](https://img.shields.io/nuget/v/Chargily.Pay.AspNet.svg)](https://www.nuget.org/packages/Chargily.Pay.AspNet/) | ![Downloads](https://img.shields.io/nuget/dt/Chargily.Pay.AspNet.svg) |

## Support with the various .NET Project-types:
Only `.NET6.0` and newer versions are supported.

_**NOTE:**  Ability to receive checkout status with Webhook endpoint is only possible with project types that can host an HTTP Server._
# Documentations Summary:
- [Installation](#installation)
- [Getting Started:](#g[.gitignore](.gitignore)etting-started)
  + [Create a checkout without Product & Price](#create-a-checkout-without-product-price)
  + [Create a Payment Link for a product](#create-a-payment-link-for-a-product)
  + [Create a Customer](#create-a-customer)
  + [Retrieve Balance Wallets](#retrieve-balance-wallets)
  + [How to Retrieve Data:](#how-to-retrieve-data)
    - [Products](#products)
    - [Prices](#prices)
    - [Customers](#customers)
    - [Checkouts](#checkouts)
    - [Checkout Items](#checkout-items)
    - [Payment Links](#payment-links)
    - [Payment Links Items](#payment-links-items)
- [Usage with ASP.NET WebApi:](#usage-with-aspnet-webapi)
  + [Install Chargily.Pay.AspNet Nuget Package](#install-chargilypayv2aspnet-nuget-package)
  + [Example Usage](#example-usage)
  + [Webhook Signature Validation](#webhook-signature-validation)


# Installation:
#### __Using DotNet CLI :__
```powershell
dotnet add Chargily.Pay
```
# Getting Started:
1. First create & configure a client:
```csharp
    using Chargily.Pay;
    
    var chargilyClient = ChargilyPay.CreateResilientClient(config =>
                                                               {
                                                                    // toggle live mode
                                                                 config.IsLiveMode = false;
                                                                    // your chargily dev account api-secret key
                                                                 config.ApiSecretKey = "YOUR API SECRET";
                                                               });
```
2. Create a Product:
```csharp
    var createProduct = new CreateProduct()
                            {
                              Name = "Product Name",
                              ImagesUrls = new List<Uri>()
                                           {
                                             new Uri("https://domain.com/image.png")
                                           },
                              Description = "Product Description",
                            };
    var product = await _chargilyPayClient.AddProduct(createProduct);
```
3. Add Price for the Product:
```csharp
    var createPrice = new CreatePrice()
                          {
                            Amount = 3000,
                            Currency = Currency.DZD,
                            ProductId = product.Value.Id,
                          };
    var productPrice = await chargilyClient.AddPrice(createPrice);
```
4. Create a checkout:
```csharp
    var checkoutItems = new List<CheckoutPriceItem>()
                            {
                              new CheckoutPriceItem()
                              {
                                Quantity = 1,
                                PriceId = productPrice.Value.Id
                              }
                            };
    var createCheckout = new Checkout(checkoutItems)
                             {
                               Description = "Checkout Description",
                               Language = LocaleType.Arabic,
                               PaymentMethod = PaymentMethod.EDAHABIA,
                               PassFeesToCustomer = true,
                               WebhookEndpointUrl = new Uri("https://domain.com/webhook/endpoint"),
                               OnFailureRedirectUrl = new Uri("https://webapp.com/checkout/fail"),
                               OnSuccessRedirectUrl = new Uri("https://webapp.com/checkout/success"),
                               CollectShippingAddress = false,
                             };
    var checkout = await chargilyClient.CreateCheckout(createCheckout);
```
### Create a checkout without Product & Price:
```csharp
    var createCheckout = new Checkout(amount: 3000, Currency.DZD)
                             {
                               Description = "Checkout Description",
                               Language = LocaleType.Arabic,
                               PaymentMethod = PaymentMethod.EDAHABIA,
                               PassFeesToCustomer = true,
                               WebhookEndpointUrl = new Uri("https://domain.com/webhook/endpoint"),
                               OnFailureRedirectUrl = new Uri("https://webapp.com/checkout/fail"),
                               OnSuccessRedirectUrl = new Uri("https://webapp.com/checkout/success"),
                               CollectShippingAddress = false,
                             };
    var fastCheckout = await chargilyClient.CreateCheckout(createCheckout);
```
_**NOTE:**  Checkout can be created with list of prices or using an [amount + currency](https://dev.chargily.com/pay-v2/api-reference/checkouts/create#body-parameters)._
### Create a Payment Link for a product:
```csharp
    var createProduct = new CreateProduct()
                        {
                            /* ... */
                        };
    var product = await _chargilyPayClient.AddProduct(createProduct);
    
    var createPrice = new CreatePrice()
                      {
                            /* ... */
                      };
    var productPrice = await chargilyClient.AddPrice(createPrice);
    // above steps are similar to how to create a checkout
    var paymentLinkItems = new List<PaymentLinkPriceItem>()
                           {
                             new PaymentLinkPriceItem()
                             {
                               AdjustableQuantity = true,
                               PriceId = productPrice.Value.Id,
                               Quantity = 2
                             }
                           };
    var createPaymentLink = new CreatePaymentLink(paymentLinkItems)
                            {
                              Language = LocaleType.Arabic,
                              PassFeesToCustomer = true,
                              CollectShippingAddress = false,
                              Name = "Name",
                              CompletionMessage = "completion message",
                              IsActive = true
                            };
```
### Create a Customer:
- Support for Customers also added in V2, and can be added to checkout also:
```csharp
    var createCustomer = new CreateCustomer()
                             {
                               Name = "Customer Name",
                               Address = new CustomerAddress()
                                         {
                                           Address = "Address",
                                           Country = Country.Algeria,
                                           State = "Alger"
                                         },
                               Email = "user@email.com",
                               Phone = "+2130601010101"
                             };
    var customer = await chargilyClient.AddCustomer(createCustomer);
    
    var createCheckout = new Checkout(amount: 3000, Currency.DZD)
                             {
                               CustomerId = customer.Value.Id,
                               /* .... */
                             };
    var fastCheckout = await chargilyClient.CreateCheckout(createCheckout);
```
### Retrieve Balance Wallets:
- Balance Wallets are refreshed automatically, to check current balance:
```csharp
    foreach (var wallet in chargilyClient.Balance)
    {
     /* ... */
    }
```
- Configuring how often balance wallets are refreshed:
```csharp
    using Chargily.Pay;
    
    var chargilyClient = ChargilyPay.CreateResilientClient(config =>
                                                               {
                                                                 /* ... */
                                                                 // refresh balance every 30 seconds 
                                                                 config.BalanceRefreshInterval = TimeSpan.FromSeconds(30);
                                                               });
```
- Or get balance manually:
```csharp
    var balance = await chargilyClient.GetBalance();
```
## How to Retrieve Data:
#### Products:
```csharp
    // by id
    var byId = await chargilyClient.GetProduct("id");
    // by page number & page size
    var products = await chargilyClient.GetProducts(page: 1, pageSize: 50);
        
    // or iterate through all items using `Async Enumerable async foreach`
    await foreach(var product in chargilyClient.Products())
    {
      /* ... */
    }
```

#### Prices:
```csharp
    // by id
    var byId = await chargilyClient.GetPrice("id");
    // by page number & page size
    var prices = await chargilyClient.GetPrices(page: 1, pageSize: 50);
    
    // or iterate through all items using `IAsyncEnumerable async foreach`
    await foreach(var price in chargilyClient.Prices())
    {
      /* ... */
    }
```

#### Customers:
```csharp
    // by id
    var byId = await chargilyClient.GetCustomer("id");
    // by page number & page size
    var customers = await chargilyClient.GetCustomers(page: 1, pageSize: 50);
    
    // or iterate through all items using `IAsyncEnumerable async foreach`
    await foreach(var customer in chargilyClient.Customers())
    {
      /* ... */
    }
```

#### Checkouts:
```csharp
    // by id
    var byId = await chargilyClient.GetCheckout("id");
    // by page number & page size
    var checkouts = await chargilyClient.GetCheckouts(page: 1, pageSize: 50);
    
    // or iterate through all items using `IAsyncEnumerable async foreach`
    await foreach(var checkout in chargilyClient.Checkouts())
    {
      /* ... */
    }
```
#### Checkout Items:
```csharp
    var checkoutItems = await chargilyClient.GetCheckoutItems("checkoutId");
```

#### Payment Links:
```csharp
    // by id
    var byId = await chargilyClient.GetPaymentLink("id");
    // by page number & page size
    var paymentLinks = await chargilyClient.GetPaymentLinks(page: 1, pageSize: 50);
    
    // or iterate through all items using `IAsyncEnumerable async foreach`
    await foreach(var paymentLink in chargilyClient.PaymentLinks())
    {
      /* ... */
    }
```

#### Payment Links Items:
```csharp
    var paymentLinksItems = await chargilyClient.GetPaymentLinkItems("paymentLinkId");
```

# Usage with ASP.NET WebApi:
### Install [`Chargily.Pay.AspNet` Nuget Package]():
```powershell
dotnet add Chargily.Pay.AspNet
```
### Example Usage:
````csharp
    using Chargily.Pay;
    using Chargily.Pay.AspNet;
    
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
    app.MapChargilyPayWebhookEndpoint("/chargily/webhook", async (webhookContext) =>
                                                           {
                                                             // you can access the webhook body, http context, validation result
                                                             if (webhookContext.SignatureIsValid)
                                                             {
                                                               var body = webhookContext.Request;
                                                               /* do something with the webhook request body */
                                                             }
                                                           });
    
    app.Run();
````
### Webhook Signature Validation
In the above example, the `Chargily.Pay.AspNet` provides built-in webhook signature validator middleware, you can register it with `builder.Services.AddChargilyPayWebhookValidationMiddleware()` then use it with `app.UseChargilyPayWebhookValidation()`.

It will validate any `POST` request that have a header name: `signature`. You can override the header name with `app.UseChargilyPayWebhookValidation("another_name")`.

Also built-in a minimal-webapi endpoint that can be registered with `app.MapChargilyPayWebhookEndpoint()`, and use it to access the webhook body without manually handling the validation.



## About Chargily Pay™ packages

Chargily Pay™ packages/plugins are a collection of open source projects published by Chargily to facilitate the integration of our payment gateway into different programming languages and frameworks. Our goal is to empower developers and businesses by providing easy-to-use tools to seamlessly accept payments.

## API Documentation

For detailed instructions on how to integrate with our API and utilize Chargily Pay™ in your projects, please refer to our [API Documentation](https://dev.chargily.com/pay-v2/introduction). 

## Developers Community

Join our developer community on Telegram to connect with fellow developers, ask questions, and stay updated on the latest news and developments related to Chargily Pay™ : [Telegram Community](https://chargi.link/PayTelegramCommunity)

## How to Contribute

We welcome contributions of all kinds, whether it's bug fixes, feature enhancements, documentation improvements, or new plugin/package developments. Here's how you can get started:

1. **Fork the Repository:** Click the "Fork" button in the top-right corner of this page to create your own copy of the repository.

2. **Clone the Repository:** Clone your forked repository to your local machine using the following command:

```bash
git clone https://github.com/Chargily/chargily-pay-csharp.git
```

3. **Make Changes:** Make your desired changes or additions to the codebase. Be sure to follow our coding standards and guidelines.

4. **Test Your Changes:** Test your changes thoroughly to ensure they work as expected.

5. **Submit a Pull Request:** Once you're satisfied with your changes, submit a pull request back to the main repository. Our team will review your contributions and provide feedback if needed.

## Get in Touch

Have questions or need assistance? Join our developer community on [Telegram](https://chargi.link/PayTelegramCommunity) and connect with fellow developers and our team.

We appreciate your interest in contributing to Chargily Pay™! Together, we can build something amazing.

Happy coding!

