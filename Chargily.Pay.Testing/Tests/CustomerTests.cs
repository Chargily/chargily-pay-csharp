namespace Chargily.Pay.Testing.Tests;

public class CustomerTests : BaseTest
{
  [Test]
  public async Task CreateCustomer_Should_Succeed()
  {
    var expected = FakeData.CreateCustomer();
    var actual = await _chargilyPayClient.AddCustomer(expected);
    Assert.Multiple(() =>
                    {
                      Assert.That(actual, Is.Not.Null);
                      Assert.That(actual.Value.Name, Is.EqualTo(expected.Name));
                      Assert.That(actual.Value.Email, Is.EqualTo(expected.Email));
                      Assert.That(actual.Value.Phone, Is.EqualTo(expected.Phone));
                      
                      Assert.That(actual.Value.Address?.Address, Is.EqualTo(expected.Address?.Address));
                      Assert.That(actual.Value.Address?.Country, Is.EqualTo(expected.Address?.Country));
                      Assert.That(actual.Value.Address?.State, Is.EqualTo(expected.Address?.State));
                    });
  }
  
  [Test]
  public async Task GetCustomer_Should_Succeed()
  {
    var expected = FakeData.CreateCustomer();
    var customer = await _chargilyPayClient.AddCustomer(expected);

    var actual = await _chargilyPayClient.GetCustomer(customer.Id);
    Assert.Multiple(() =>
                    {
                      Assert.That(actual!.Value, Is.Not.Null);
                      Assert.That(actual.Value.Name, Is.EqualTo(expected.Name));
                      Assert.That(actual.Value.Email, Is.EqualTo(expected.Email));
                      Assert.That(actual.Value.Phone, Is.EqualTo(expected.Phone));
                      
                      Assert.That(actual.Value.Address?.Address, Is.EqualTo(expected.Address?.Address));
                      Assert.That(actual.Value.Address?.Country, Is.EqualTo(expected.Address?.Country));
                      Assert.That(actual.Value.Address?.State, Is.EqualTo(expected.Address?.State));
                    });
  }
  
  [Test]
  public async Task Enumerate_Customers_Should_Succeed()
  {
    var expected = FakeData.CreateCustomer();
    await _chargilyPayClient.AddCustomer(expected);

    var firstPage = await _chargilyPayClient.GetCustomers();
    var actual = _chargilyPayClient.Customers();
    
    Assert.Multiple(async () =>
                    {
                      var count = 0; 
                      await foreach (var customer in actual)
                      {
                        Assert.That(customer, Is.Not.Null);
                        count++;
                      }
                      Assert.That(count, Is.EqualTo(firstPage.Total));
                    });
  }
}