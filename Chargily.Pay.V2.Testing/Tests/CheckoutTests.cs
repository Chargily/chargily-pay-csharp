using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Testing.Tests;

public class CheckoutTests : BaseTest
{
  
  [Test]
  public async Task CreateCheckout_WithCustomer_WithItems_Should_Succeed()
  {
    var customer = FakeData.CreateCustomer();
    var actualCustomer = await _chargilyPayClient.AddCustomer(customer);
    
    var product = FakeData.CreateProduct();
    var actualProduct = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(actualProduct.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreateCheckoutPriceItem(actualPrice.Id);
    var expected = FakeData.CheckoutWithItems([item],f => f.RuleFor(x => x.CustomerId, _ => actualCustomer.Id));
    
    var actual = await _chargilyPayClient.CreateCheckout(expected);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Description, Is.EqualTo(expected.Description));
                      Assert.That(actual.Value.CustomerId, Is.EqualTo(expected.CustomerId));
                      Assert.That(actual.Value.PaymentMethod, Is.EqualTo(expected.PaymentMethod));
                      Assert.That(actual.Value.WebhookEndpointUrl, Is.EqualTo(expected.WebhookEndpointUrl));
                      Assert.That(actual.Value.OnSuccessRedirectUrl, Is.EqualTo(expected.OnSuccessRedirectUrl));
                      Assert.That(actual.Value.OnFailureRedirectUrl, Is.EqualTo(expected.OnFailureRedirectUrl));
                      Assert.That(actual.Value.PassFeesToCustomer, Is.EqualTo(expected.PassFeesToCustomer));
                    });
  }
  [Test]
  public async Task CreateCheckout_WithCustomer_WithoutItems_Should_Succeed()
  {
    var customer = FakeData.CreateCustomer();
    var actualCustomer = await _chargilyPayClient.AddCustomer(customer);
    
    var expected = FakeData.Checkout(f => f.RuleFor(x => x.CustomerId, _ => actualCustomer.Id));

    var actual = await _chargilyPayClient.CreateCheckout(expected);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Amount, Is.EqualTo(expected.Amount));
                      Assert.That(actual.Value.Description, Is.EqualTo(expected.Description));
                      Assert.That(actual.Value.Currency, Is.EqualTo(expected.Currency));
                      Assert.That(actual.Value.CustomerId, Is.EqualTo(expected.CustomerId));
                      Assert.That(actual.Value.PaymentMethod, Is.EqualTo(expected.PaymentMethod));
                      Assert.That(actual.Value.WebhookEndpointUrl, Is.EqualTo(expected.WebhookEndpointUrl));
                      Assert.That(actual.Value.OnSuccessRedirectUrl, Is.EqualTo(expected.OnSuccessRedirectUrl));
                      Assert.That(actual.Value.OnFailureRedirectUrl, Is.EqualTo(expected.OnFailureRedirectUrl));
                      Assert.That(actual.Value.PassFeesToCustomer, Is.EqualTo(expected.PassFeesToCustomer));
                    });
  }
  
  [Test]
  public async Task CreateCheckout_WithoutItems_Should_Succeed()
  {
    var customer = FakeData.CreateCustomer();
    var actualCustomer = await _chargilyPayClient.AddCustomer(customer);
    
    var expected = FakeData.Checkout();

    var actual = await _chargilyPayClient.CreateCheckout(expected);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Amount, Is.EqualTo(expected.Amount));
                      Assert.That(actual.Value.Description, Is.EqualTo(expected.Description));
                      Assert.That(actual.Value.Currency, Is.EqualTo(expected.Currency));
                      Assert.That(actual.Value.CustomerId, Is.EqualTo(expected.CustomerId));
                      Assert.That(actual.Value.PaymentMethod, Is.EqualTo(expected.PaymentMethod));
                      Assert.That(actual.Value.WebhookEndpointUrl, Is.EqualTo(expected.WebhookEndpointUrl));
                      Assert.That(actual.Value.OnSuccessRedirectUrl, Is.EqualTo(expected.OnSuccessRedirectUrl));
                      Assert.That(actual.Value.OnFailureRedirectUrl, Is.EqualTo(expected.OnFailureRedirectUrl));
                      Assert.That(actual.Value.PassFeesToCustomer, Is.EqualTo(expected.PassFeesToCustomer));
                    });
  }
  
  
  [Test]
  public async Task CreateCheckout_WithItems_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var actualProduct = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(actualProduct.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreateCheckoutPriceItem(actualPrice.Id);
    var expected = FakeData.CheckoutWithItems([item]);
    
    var actual = await _chargilyPayClient.CreateCheckout(expected);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Description, Is.EqualTo(expected.Description));
                      Assert.That(actual.Value.CustomerId, Is.EqualTo(expected.CustomerId));
                      Assert.That(actual.Value.PaymentMethod, Is.EqualTo(expected.PaymentMethod));
                      Assert.That(actual.Value.WebhookEndpointUrl, Is.EqualTo(expected.WebhookEndpointUrl));
                      Assert.That(actual.Value.OnSuccessRedirectUrl, Is.EqualTo(expected.OnSuccessRedirectUrl));
                      Assert.That(actual.Value.OnFailureRedirectUrl, Is.EqualTo(expected.OnFailureRedirectUrl));
                      Assert.That(actual.Value.PassFeesToCustomer, Is.EqualTo(expected.PassFeesToCustomer));
                    });
  }

  [Test]
  public async Task GetCheckout_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var actualProduct = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(actualProduct.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreateCheckoutPriceItem(actualPrice.Id);
    var expected = FakeData.CheckoutWithItems([item]);

    var created = await _chargilyPayClient.CreateCheckout(expected);
    var actual = await _chargilyPayClient.GetCheckout(created.Id);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual!.Value.Id, Is.EqualTo(created.Id));
                      Assert.That(actual.Value.Description, Is.EqualTo(expected.Description));
                      Assert.That(actual.Value.CustomerId, Is.EqualTo(expected.CustomerId));
                      Assert.That(actual.Value.PaymentMethod, Is.EqualTo(expected.PaymentMethod));
                      Assert.That(actual.Value.WebhookEndpointUrl, Is.EqualTo(expected.WebhookEndpointUrl));
                      Assert.That(actual.Value.OnSuccessRedirectUrl, Is.EqualTo(expected.OnSuccessRedirectUrl));
                      Assert.That(actual.Value.OnFailureRedirectUrl, Is.EqualTo(expected.OnFailureRedirectUrl));
                      Assert.That(actual.Value.PassFeesToCustomer, Is.EqualTo(expected.PassFeesToCustomer));
                    });
  }
  
  [Test]
  public async Task Enumerate_Checkouts_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var actualProduct = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(actualProduct.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreateCheckoutPriceItem(actualPrice.Id);
    var expected = FakeData.CheckoutWithItems([item]);

    await _chargilyPayClient.CreateCheckout(expected);

    var firstPage = await _chargilyPayClient.GetCheckouts();
    var actual = _chargilyPayClient.Checkouts();
    
    Assert.Multiple(async () =>
                    {
                      var count = 0; 
                      await foreach (var checkout in actual)
                      {
                        Assert.That(checkout, Is.Not.Null);
                        count++;
                      }
                      Assert.That(count, Is.EqualTo(firstPage.Total));
                    });
  }
  
  [Test]
  public async Task CancelCheckout_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var actualProduct = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(actualProduct.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreateCheckoutPriceItem(actualPrice.Id);
    var expected = FakeData.CheckoutWithItems([item]);
    
    var created = await _chargilyPayClient.CreateCheckout(expected);
    var actual = await _chargilyPayClient.CancelCheckout(created.Id);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual!.Id, Is.EqualTo(created.Id));
                      Assert.That(actual.Status, Is.AnyOf([CheckoutStatus.Canceled, CheckoutStatus.Expired]));
                    });
  }
}