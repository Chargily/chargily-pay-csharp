using Bogus;
using Chargily.Pay.V2.Models;
using NanoidDotNet;
using SakontStack.FunctionalExtensions;

namespace Chargily.Pay.V2.Testing;

public class FakeData
{
  public static CreateProduct CreateProduct(Func<Faker<CreateProduct>, Faker<CreateProduct>>? modifier = null)
  {
    return new Faker<CreateProduct>()
          .CustomInstantiator(f => new CreateProduct()
                                   {
                                     Metadata = ["Metadata" ],
                                     Description = f.Random.String2(10),
                                     ImagesUrls = [new Uri("https://i.imgur.com/rZ47kAY.png")],
                                     Name = f.Commerce.Product()
                                   })
          .Map(x => modifier?.Invoke(x) ?? x)
          .Generate();
  }

  public static CreatePrice CreatePrice(string productId, Func<Faker<CreatePrice>, Faker<CreatePrice>>? modifier = null)
  {
    return new Faker<CreatePrice>()
          .CustomInstantiator(f => new CreatePrice()
                                   {
                                     Amount = f.Finance.Amount(100, 2000),
                                     Currency = Currency.DZD,
                                     ProductId = productId,
                                     Metadata = ["Metadata" ],
                                   })
          .Map(x => modifier?.Invoke(x) ?? x)
          .Generate();
  }

  public static Checkout Checkout(Func<Faker<Checkout>, Faker<Checkout>>? modifier = null)
  {
    return new Faker<Checkout>()
          .CustomInstantiator(f => new Checkout(f.Finance.Amount(100, 2000), Currency.DZD)
                                   {
                                     Metadata = ["Metadata" ],
                                     Description = f.Random.String2(10),
                                     PaymentMethod = f.PickRandom<PaymentMethod>(),
                                     WebhookEndpointUrl = new Uri(f.Internet.UrlWithPath("https", "myapp.com")),
                                     OnFailureRedirectUrl = new Uri(f.Internet.UrlWithPath("https", "myapp.com")),
                                     OnSuccessRedirectUrl = new Uri(f.Internet.UrlWithPath("https", "myapp.com")),
                                     PassFeesToCustomer = f.PickRandom(true, false),
                                     Language = f.PickRandom<LocaleType>(),
                                   })
          .Map(x => modifier?.Invoke(x) ?? x)
          .Generate();
  }

  public static CheckoutPriceItem CreateCheckoutPriceItem(string priceId, Func<Faker<CheckoutPriceItem>, Faker<CheckoutPriceItem>>? modifier = null)
  {
    return new Faker<CheckoutPriceItem>()
          .CustomInstantiator(f => new CheckoutPriceItem()
                                   {
                                     Quantity = f.Random.Number(1, 20),
                                     PriceId = priceId
                                   })
          .Map(x => modifier?.Invoke(x) ?? x)
          .Generate();
  }

  public static Checkout CheckoutWithItems(List<CheckoutPriceItem> items, Func<Faker<Checkout>, Faker<Checkout>>? modifier = null)
  {
    return new Faker<Checkout>()
          .CustomInstantiator(f => new Checkout(items)
                                   {
                                     Metadata = ["Metadata" ],
                                     Description = f.Random.String2(10),
                                     PaymentMethod = f.PickRandom<PaymentMethod>(),
                                     WebhookEndpointUrl = new Uri(f.Internet.UrlWithPath("https", "myapp.com")),
                                     OnFailureRedirectUrl = new Uri(f.Internet.UrlWithPath("https", "myapp.com")),
                                     OnSuccessRedirectUrl = new Uri(f.Internet.UrlWithPath("https", "myapp.com")),
                                     PassFeesToCustomer = f.PickRandom(true, false),
                                     Language = f.PickRandom<LocaleType>(),
                                   })
          .Map(x => modifier?.Invoke(x) ?? x)
          .Generate();
  }

  public static CreatePaymentLink CreatePaymentLink(Func<Faker<CreatePaymentLink>, Faker<CreatePaymentLink>>? modifier = null)
  {
    return new Faker<CreatePaymentLink>()
          .CustomInstantiator(f => new CreatePaymentLink()
                                   {
                                     Metadata = ["Metadata" ],
                                     PassFeesToCustomer = f.PickRandom(true, false),
                                     Language = f.PickRandom<LocaleType>(),
                                     Name = f.Random.String2(10),
                                     CompletionMessage = f.Random.String2(10),
                                     IsActive = f.PickRandom(true, false)
                                   })
          .Map(x => modifier?.Invoke(x) ?? x)
          .Generate();
  }

  public static CreateCustomer CreateCustomer(Func<Faker<CreateCustomer>, Faker<CreateCustomer>>? modifier = null)
  {
    return new Faker<CreateCustomer>()
          .CustomInstantiator(f => new CreateCustomer()
                                   {
                                     Metadata = ["Metadata" ],
                                     Name = f.Person.FullName,
                                     Address = new CustomerAddress()
                                               {
                                                 Address = f.Address.FullAddress(),
                                                 Country = f.PickRandom<Country>(),
                                                 State = f.Address.State()
                                               },
                                     Email = f.Internet.Email(),
                                     Phone = $"+21306{f.Random.String2(8,"0123456789")}"
                                   })
          .Map(x => modifier?.Invoke(x) ?? x)
          .Generate();
  }
}