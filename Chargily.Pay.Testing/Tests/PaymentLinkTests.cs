namespace Chargily.Pay.Testing.Tests;

public class PaymentLinkTests : BaseTest
{
  [Test]
  public async Task CreatePaymentLink_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var productActual = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(productActual.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreatePaymentLinkPriceItem(actualPrice.Id);
    
    var expected = FakeData.CreatePaymentLink([item]);
    var actual = await _chargilyPayClient.CreatePaymentLink(expected);

    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Name, Is.EqualTo(expected.Name));
                      Assert.That(actual.Value.IsActive, Is.EqualTo(expected.IsActive));
                      Assert.That(actual.Value.Language, Is.EqualTo(expected.Language));
                      Assert.That(actual.Value.CompletionMessage, Is.EqualTo(expected.CompletionMessage));

                    });
  }
  
  [Test]
  public async Task GetPaymentLink_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var productActual = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(productActual.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreatePaymentLinkPriceItem(actualPrice.Id);
    
    var model = FakeData.CreatePaymentLink([item]);
    var expected = await _chargilyPayClient.CreatePaymentLink(model);

    var actual = await _chargilyPayClient.GetPaymentLink(expected.Id);

    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Id, Is.EqualTo(expected.Value.Id));
                      Assert.That(actual.Value.Name, Is.EqualTo(expected.Value.Name));
                      Assert.That(actual.Value.IsActive, Is.EqualTo(expected.Value.IsActive));
                      Assert.That(actual.Value.Language, Is.EqualTo(expected.Value.Language));
                      Assert.That(actual.Value.CompletionMessage, Is.EqualTo(expected.Value.CompletionMessage));
                      Assert.That(actual.Value.Url, Is.EqualTo(expected.Value.Url));

                    });
  }
  
  [Test]
  public async Task Enumerate_PaymentLinks_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var productActual = await _chargilyPayClient.AddProduct(product);
    
    var price = FakeData.CreatePrice(productActual.Id);
    var actualPrice = await _chargilyPayClient.AddPrice(price);
    
    var item = FakeData.CreatePaymentLinkPriceItem(actualPrice.Id);
    
    var model = FakeData.CreatePaymentLink([item]);
   await _chargilyPayClient.CreatePaymentLink(model);

    var firstPage = await _chargilyPayClient.GetPaymentLinks();
    var actual = _chargilyPayClient.PaymentLinks();
    
    Assert.Multiple(async () =>
                    {
                      var count = 0; 
                      await foreach (var paymentLink in actual)
                      {
                        Assert.That(paymentLink, Is.Not.Null);
                        count++;
                      }
                      Assert.That(count, Is.EqualTo(firstPage.Total));
                    });
  }
}