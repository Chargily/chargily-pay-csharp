using System.Text;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal;

internal static class CacheKey
{
    private static readonly string Separator = char.ConvertFromUtf32(17);

    public static string From(EntityType entityType, ChargilyConfig config, string id)
    {
        return new StringBuilder()
              .AppendJoin(Separator,
                          [
                              config.ApiSecretKey,
                              config.IsLiveMode.ToString(),
                              Enum.GetName(entityType),
                              id
                          ])
              .ToString();
    }
    public static string From(EntityType entityType, ChargilyConfig config, params string[] parameters)
    {
        return new StringBuilder()
              .AppendJoin(Separator,
                          [
                              config.ApiSecretKey,
                              config.IsLiveMode.ToString(),
                              Enum.GetName(entityType),
                              ..parameters
                          ])
              .ToString();
    }
    public static string From(EntityType entityType, ChargilyConfig config)
    {
        return new StringBuilder()
              .AppendJoin(Separator,
                          [
                              config.ApiSecretKey,
                              config.IsLiveMode.ToString(),
                              Enum.GetName(entityType),
                          ])
              .ToString();
    }
    
};