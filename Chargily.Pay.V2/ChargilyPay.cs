﻿using System.Net.Http.Headers;
using Chargily.Pay.V2.Abstractions;
using Chargily.Pay.V2.Exceptions;
using Chargily.Pay.V2.Internal;
using Chargily.Pay.V2.Internal.Endpoints;
using Chargily.Pay.V2.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;

namespace Chargily.Pay.V2;

public static class ChargilyPay
{
  public static IChargilyPayClient CreateResilientClient(Action<ChargilyConfig> configure, Action<ILoggingBuilder>? configureLogging = null)
  {
    var config = new ChargilyConfig();
    configure.Invoke(config);
    if (string.IsNullOrWhiteSpace(config.ApiSecretKey))
      throw new ChargilyPayApiSecretNotFoundException();

    var serviceProvider = new ServiceCollection()
                         .AddSingleton<IOptions<ChargilyConfig>>(Options.Create(config))
                         .AddMemoryCache()
                         .AddAutoMapper(x => x.AddMaps(typeof(ResilientChargilyPayClient).Assembly))
                         .AddValidatorsFromAssembly(typeof(ResilientChargilyPayClient).Assembly)
                         .AddLogging(logConfig =>
                                     {
                                       logConfig.AddDebug();
                                       logConfig.SetMinimumLevel(config.IsLiveMode ? LogLevel.Information : LogLevel.Debug);
                                       configureLogging?.Invoke(logConfig);
                                     })
                         .AddHttpClient()
                         .AddRefitClient<IChargilyPayApi>(provider =>
                                                          {
                                                            var logger = provider.GetRequiredService<ILogger<IChargilyPayClient>>();
                                                            return new RefitSettings()
                                                                   {
                                                                     // AuthorizationHeaderValueGetter =
                                                                     //   (_, _) => { return Task.FromResult($"Bearer {config.ApiSecretKey}"); },
                                                                     ExceptionFactory = async (message) =>
                                                                                        {
                                                                                          if (message.IsSuccessStatusCode) return null;
                                                                                          var response = await message.Content.ReadAsStringAsync();
                                                                                          return new ChargilyPayApiException((int)message.StatusCode, response);
                                                                                        }
                                                                   };
                                                          })
                         .ConfigureHttpClient(client =>
                                              {
                                                client.Timeout = config.RequestTimeout;
                                                client.BaseAddress = new Uri(config.IsLiveMode
                                                                               ? "https://pay.chargily.net/api/v2"
                                                                               : "https://pay.chargily.net/test/api/v2");
                                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",config.ApiSecretKey);
                                              })
                         .Services
                         .AddSingleton<IChargilyPayClient, ResilientChargilyPayClient>()
                         .AddSingleton<IWebhookValidator, ChargilyPayWebhookValidator>()
                         .BuildServiceProvider();
    var client = serviceProvider.GetRequiredService<IChargilyPayClient>();
    ((client as ResilientChargilyPayClient)!).OnDisposing += () => serviceProvider.Dispose();
    return client;
  }
}