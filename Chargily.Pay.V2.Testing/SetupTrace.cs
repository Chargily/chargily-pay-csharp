using System.Diagnostics;

namespace Chargily.Pay.V2.Testing;

[SetUpFixture]
public class SetupTrace
{
  [OneTimeSetUp]
  public void StartTest()
  {
    Trace.Listeners.Add(new ConsoleTraceListener());
  }

  [OneTimeTearDown]
  public void EndTest()
  {
    Trace.Flush();
  }
}