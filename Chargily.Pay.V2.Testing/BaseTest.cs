using Chargily.Pay.V2.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Chargily.Pay.V2.Testing;

public class BaseTest
{
  internal IChargilyPayClient _chargilyPayClient;

  [SetUp]
  public void Setup()
  {
    var apiKey = Environment.GetEnvironmentVariable("CHARGILY_SECRET_KEY");
    Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.NUnitOutput(restrictedToMinimumLevel: LogEventLevel.Debug)
                .CreateLogger();

    _chargilyPayClient = ChargilyPay.CreateResilientClient(config =>
                                                           {
                                                             config.IsLiveMode = false;
                                                             config.ApiSecretKey = apiKey;
                                                           },
                                                           log =>
                                                           {
                                                             log.AddSerilog();
                                                             log.SetMinimumLevel(LogLevel.Debug);
                                                           });
  }

  [TearDown]
  public void TearDown()
  {
    _chargilyPayClient?.Dispose();
  }
}