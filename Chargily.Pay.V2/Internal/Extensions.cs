using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chargily.Pay.V2.Internal;

public static class Extensions
{
    
    internal static int? GetPage(this Uri uri)
    {
        return Convert.ToInt32(HttpUtility
                              .ParseQueryString(uri.Query)
                              .Get("page"));
    }
    
    internal static int GetPageOrDefault(this string uriString, int defaultValue)
    {
        return Uri.TryCreate(uriString, UriKind.Absolute, out var url)
            ? url.GetPage()! ?? defaultValue
            : defaultValue;
    }
    
    internal static int? GetPage(this string uriString)
    {
        return Uri.TryCreate(uriString, UriKind.Absolute, out var url)
                   ? url.GetPage()!
                   : null;
    }

    public static void LogValidationErrorsAndThrow(this ValidationResult validationResult, ILogger logger)
    {
        var errors = validationResult.Errors.Stringify();
        logger?.LogError("Validations failed!:\n{@errors}", errors);
        throw new Exception(errors);
    }

    public static string Stringify<T>(this T value, bool removeNullFields = false) where T : class
    {
        return JsonSerializer.Serialize(value,
                                        new JsonSerializerOptions()
                                        {
                                            WriteIndented = true,
                                            DefaultIgnoreCondition =
                                                removeNullFields
                                                    ? JsonIgnoreCondition.WhenWritingNull
                                                    : JsonIgnoreCondition.Never
                                        });
    }
}