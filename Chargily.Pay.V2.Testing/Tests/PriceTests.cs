using SakontStack.FunctionalExtensions;

namespace Chargily.Pay.V2.Testing.Tests;

public class PriceTests : BaseTest
{
  [Test]
  public async Task CreatePrice_Should_Succeed()
  {
    var product = FakeData.CreateProduct();
    var productActual = await _chargilyPayClient.AddProduct(product);

    var expected = FakeData.CreatePrice(f => f.RuleFor(x => x.ProductId, productActual.Id));
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

    var price = FakeData.CreatePrice(f => f.RuleFor(x => x.ProductId, productActual.Id));
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
}