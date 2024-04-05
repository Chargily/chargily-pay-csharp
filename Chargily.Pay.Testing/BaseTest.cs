using Chargily.Pay.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Chargily.Pay.Testing;

public class BaseTest
{
  internal IChargilyPayClient _chargilyPayClient;

  [SetUp]
  public void Setup()
  {
    var configuration = new ConfigurationBuilder()
                       .AddEnvironmentVariables()
                       .AddUserSecrets<BaseTest>()
                       .Build();
    var apiSecret = configuration["CHARGILY_SECRET_KEY"]!;

    Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.NUnitOutput(restrictedToMinimumLevel: LogEventLevel.Debug)
                .CreateLogger();

    _chargilyPayClient = ChargilyPay.CreateResilientClient(config =>
                                                           {
                                                             config.IsLiveMode = false;
                                                             config.ApiSecretKey = apiSecret!;
                                                           },
                                                           log =>
                                                           {
                                                             log.AddSerilog();
                                                             log.SetMinimumLevel(LogLevel.Debug);
                                                           });
  }


  public async Task Cleanup()
  {
    await foreach (var product in _chargilyPayClient.Products())
    {
      try
      {
        await _chargilyPayClient.DeleteProduct(product.Id);
      }
      catch (Exception e)
      {
        Log.Logger.Warning(e.ToString());
      }
    }
    await foreach (var customer in _chargilyPayClient.Customers())
    {
      try
      {
        await _chargilyPayClient.DeleteCustomer(customer!.Id);
      }
      catch (Exception e)
      {
        Log.Logger.Warning(e.ToString());
      }
    }
    await foreach (var checkout in _chargilyPayClient.Checkouts())
    {
      try
      {
        await _chargilyPayClient.CancelCheckout(checkout!.Id);
      }
      catch (Exception e)
      {
        Log.Logger.Warning(e.ToString());
      }
    }
  }
  
  [TearDown]
  public async Task TearDown()
  {
    await Cleanup();
    _chargilyPayClient?.Dispose();
  }
}