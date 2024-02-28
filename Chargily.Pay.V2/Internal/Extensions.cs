using System.Text.Json;
using System.Web;
using FluentValidation.Results;
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

    public static void LogValidationErrorsAndThrow(this ValidationResult validationResult, ILogger logger)
    {
        var errors = validationResult.Errors.Stringify();
        logger?.LogError("Validations failed!:\n{@errors}", errors);
        throw new Exception(errors);
    }

    public static string Stringify<T>(this T value) where T: class
    {
        return JsonSerializer.Serialize(value, new JsonSerializerOptions() { WriteIndented = true });
    }
}