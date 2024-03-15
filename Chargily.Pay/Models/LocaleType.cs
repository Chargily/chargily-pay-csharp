namespace Chargily.Pay.Models;

public enum LocaleType
{
  /// <summary>
  /// AR
  /// </summary>
  Arabic,

  /// <summary>
  /// EN
  /// </summary>
  English,

  /// <summary>
  /// FR
  /// </summary>
  French
}

public static class Language
{
  private static readonly Dictionary<LocaleType, string> Languages = new Dictionary<LocaleType, string>()
                                                                     {
                                                                       { LocaleType.Arabic, "ar" },
                                                                       { LocaleType.English, "en" },
                                                                       { LocaleType.French, "fr" },
                                                                     };

  private static readonly Dictionary<string, LocaleType> LocaleTypes = new Dictionary<string, LocaleType>()
                                                                       {
                                                                         { "ar", LocaleType.Arabic },
                                                                         { "en", LocaleType.English },
                                                                         { "fr", LocaleType.French },
                                                                       };

  public static string? GetLanguage(LocaleType localeType)
    => Languages.GetValueOrDefault(localeType);

  public static LocaleType? GetLocalType(string language)
    => LocaleTypes.GetValueOrDefault(language);
}