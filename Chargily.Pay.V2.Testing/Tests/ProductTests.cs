namespace Chargily.Pay.V2.Testing.Tests;

public class ProductTests : BaseTest
{
  [Test]
  public async Task CreateProduct_Should_Succeed()
  {
    var expected = FakeData.CreateProduct();

    var actual = await _chargilyPayClient.AddProduct(expected);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Name, Is.EqualTo(expected.Name));
                      Assert.That(actual.Value.Description, Is.EqualTo(expected.Description));
                    });
  }
  
  [Test]
  public async Task GetProduct_Should_Succeed()
  {
    var product = FakeData.CreateProduct();

    var expected = await _chargilyPayClient.AddProduct(product);
    var actual = await _chargilyPayClient.GetProduct(expected.Id);
    
    Assert.Multiple(() =>
                    {
                      Assert.That(expected, Is.Not.Null);
                      Assert.That(actual!.Value, Is.Not.Null);
                      Assert.That(actual.Id, Is.EqualTo(expected.Id));
                    });
  }
  
  [Test]
  public async Task Enumerate_Products_Should_Succeed()
  {
    var expected = Enumerable
                  .Range(0, 3)
                  .Select(_ => FakeData.CreateProduct())
                  .ToList();
    foreach (var product in expected)
    {
      await _chargilyPayClient.AddProduct(product);
    }

    var firstPage = await _chargilyPayClient.GetProducts();
    var actual = _chargilyPayClient.Products();
    
    Assert.Multiple(async () =>
                    {
                      var count = 0; 
                      await foreach (var product in actual)
                      {
                        Assert.That(product, Is.Not.Null);
                        count++;
                      }
                      Assert.That(count, Is.EqualTo(firstPage.Total));
                    });
  }
}