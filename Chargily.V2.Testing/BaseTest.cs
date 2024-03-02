using Chargily.Pay.V2;
using Chargily.Pay.V2.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Chargily.V2.Testing;

public class BaseTest
{
  internal IChargilyPayClient _chargilyPayClient;

  [SetUp]
  public void Setup()
  {
    var apiKey = "[API_SECRET_KEY]";
    Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .WriteTo.Console(theme: AnsiConsoleTheme.Code,
                                 restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

    _chargilyPayClient = ChargilyPay.CreateResilientClient(config =>
                                                           {
                                                             config.IsLiveMode = true;
                                                             config.ApiSecretKey = apiKey;
                                                           },
                                                           log => log.AddSerilog());
  }

  [TearDown]
  public void TearDown()
  {
    _chargilyPayClient?.Dispose();
  }
}