<img src="https://raw.githubusercontent.com/rainxh11/chargily-pay-csharp/main/Assets/chargily_wide.svg" width="300"/>

| Nuget Pacakge                                                                                                                                                  | Downloads                                                                |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------|
| Chargily.Pay.V2: <br/>[![Latest version](https://img.shields.io/nuget/v/Chargily.Pay.V2.svg)](https://www.nuget.org/packages/Chargily.Pay.V2/)                      | ![Downloads](https://img.shields.io/nuget/dt/Chargily.Pay.V2.svg)        |
| Chargily.Pay.V2.AspNet:<br/> [![Latest version](https://img.shields.io/nuget/v/Chargily.Pay.V2.AspNet.svg)](https://www.nuget.org/packages/Chargily.Pay.V2.AspNet/) | ![Downloads](https://img.shields.io/nuget/dt/Chargily.Pay.V2.AspNet.svg) |

# Chargily Pay V2 .NET Client Library
A fully-featured client library to work with **Chargily Pay API version 2** Online Payment Platform. The easiest and free way to integrate e-payment API through **EDAHABIA** of Algerie Poste and **CIB** of **SATIM**
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
  + [Install Chargily.Pay.V2.AspNet Nuget Package](#install-chargilypayv2aspnet-nuget-package)
  + [Example Usage](#example-usage)


# Installation:
#### __Using DotNet CLI :__
```powershell
dotnet add Chargily.Pay.V2
```
# Getting Started:
1. First create & configure a client:
```csharp
    using Chargily.Pay.V2;
    
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
    using Chargily.Pay.V2;
    
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
### Install [`Chargily.Pay.V2.AspNet` Nuget Package](https://www.nuget.org/packages/Chargily.Pay.V2.AspNet):
```powershell
dotnet add Chargily.Pay.V2.AspNet
```
### Example Usage:
````csharp
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
````
