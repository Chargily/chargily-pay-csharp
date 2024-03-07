using SakontStack.FunctionalExtensions;

namespace Chargily.Pay.V2.Testing.Tests;

public class PriceTests : BaseTest
{
  [Test]
  public async Task CreatePrice_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var productActual = await _chargilyPayClient.AddProduct(product);

    var expected = FakeData.CreatePrice(productActual.Id);
    var actual = await _chargilyPayClient.AddPrice(expected);

    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Amount, Is.EqualTo(expected.Amount));
                      Assert.That(actual.Value.Currency, Is.EqualTo(expected.Currency));
                      Assert.That(actual.Value.ProductId, Is.EqualTo(expected.ProductId));
                      Assert.That(actual.Value.Product, Is.Not.Null);
                    });
  }
  
  [Test]
  public async Task GetProduct_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var productActual = await _chargilyPayClient.AddProduct(product);

    var price = FakeData.CreatePrice(productActual.Id);
    var expected = await _chargilyPayClient.AddPrice(price);
    var actual = await _chargilyPayClient.GetPrice(expected.Id);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(expected, Is.Not.Null);
                      Assert.That(actual!.Value, Is.Not.Null);
                      Assert.That(actual.Id, Is.EqualTo(expected.Id));
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Amount, Is.EqualTo(expected.Value.Amount));
                      Assert.That(actual.Value.Currency, Is.EqualTo(expected.Value.Currency));
                      Assert.That(actual.Value.ProductId, Is.EqualTo(expected.Value.ProductId));
                      Assert.That(actual.Value.Product, Is.Not.Null);
                    });
  }
  
  [Test]
  public async Task Enumerate_Prices_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var productActual = await _chargilyPayClient.AddProduct(product);

    var priceModel = FakeData.CreatePrice(productActual.Id);
    await _chargilyPayClient.AddPrice(priceModel);

    var firstPage = await _chargilyPayClient.GetPrices();
    var actual = _chargilyPayClient.Prices();
    
    Assert.Multiple(async () =>
                    {
                      var count = 0; 
                      await foreach (var price in actual)
                      {
                        Assert.That(price, Is.Not.Null);
                        count++;
                      }
                      Assert.That(count, Is.EqualTo(firstPage.Total));
                    });
  }
}