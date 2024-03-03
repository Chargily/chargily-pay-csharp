namespace Chargily.Pay.V2.Testing.Tests;

public class BalanceTests : BaseTest
{
  [Test]
  public void GetBalance_Should_Succeed()
  {
    Assert.Multiple(() =>
                    {
                      Assert.DoesNotThrowAsync(() => _chargilyPayClient.GetBalance());
                      Assert.That(_chargilyPayClient.Balance, Is.Not.Null);
                      Assert.That(_chargilyPayClient.Balance, Is.Not.Count.Zero);
                      Assert.That(_chargilyPayClient.BalanceRefreshedAt, Is.Not.Null);
                    });
  }
}