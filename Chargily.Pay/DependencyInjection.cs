using Chargily.Pay.Abstractions;
using Chargily.Pay.Exceptions;
using Chargily.Pay.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chargily.Pay;

public static class DependencyInjection
{
  /// <summary>
  /// add <b>ResilientChargilyPayClient</b> global instance
  /// </summary>
  /// <param name="services"></param>
  /// <param name="configure"></param>
  /// <param name="configureLogging"></param>
  /// <returns>IServiceCollection</returns>
  /// <exception cref="ChargilyPayApiSecretNotFoundException">Missing API Secret Key</exception>
  /// <exception cref="ChargilyPayClientAlreadyRegistered">Client Already Registered</exception>
  public static IServiceCollection AddGlobalChargilyPayClient(this IServiceCollection services,
                                                              Action<ChargilyConfig> configure,
                                                              Action<ILoggingBuilder>? configureLogging = null)
  {
    if (services.Any(x => x.ServiceType == typeof(IChargilyPayClient)))
      throw new ChargilyPayClientAlreadyRegistered();

    var client = ChargilyPay.CreateResilientClient(configure, configureLogging);
    return services.AddSingleton<IChargilyPayClient>(client)
                   .AddSingleton<IWebhookValidator>(client.WebhookValidator);
  }

  /// <summary>
  /// add <b>ResilientChargilyPayClient</b> instance with API Secret as Key
  /// </summary>
  /// <param name="services">Services Collection</param>
  /// <param name="configure">Configure Chargily Pay</param>
  /// <param name="configureLogging">Configure Logging</param>
  /// <returns>IServiceCollection</returns>
  /// <exception cref="ChargilyPayApiSecretNotFoundException">Missing API Secret Key</exception>
  /// <exception cref="ChargilyPayClientAlreadyRegistered">Client Already Registered</exception>
  public static IServiceCollection AddChargilyPayClientKeyed(this IServiceCollection services,
                                                             Action<ChargilyConfig> configure,
                                                             Action<ILoggingBuilder>? configureLogging = null)
  {
    var config = new ChargilyConfig();
    configure.Invoke(config);

    if (string.IsNullOrWhiteSpace(config.ApiSecretKey))
      throw new ChargilyPayApiSecretNotFoundException();

    if (services.Any(x => x.ServiceType == typeof(IChargilyPayClient) &&
                          x.IsKeyedService &&
                          (string)x.ServiceKey! == config.ApiSecretKey))
      throw new ChargilyPayClientAlreadyRegistered(config.ApiSecretKey);

    var client = ChargilyPay.CreateResilientClient(configure, configureLogging);
    return services.AddKeyedSingleton<IChargilyPayClient>(config.ApiSecretKey, client)
                   .AddKeyedSingleton<IWebhookValidator>(config.ApiSecretKey, client.WebhookValidator);
  }
}