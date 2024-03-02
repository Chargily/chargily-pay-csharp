namespace Chargily.V2.Testing;

public class BalanceTests : BaseTest
{
  [Test]
  public void GetBalance_Should_Succeed()
  {
    Assert.Multiple(() =>
                    {
                      Assert.DoesNotThrowAsync(() => _chargilyPayClient.GetBalance());
                      Assert.That(_chargilyPayClient.Balance, Is.Not.Null);
                    });
  }
}