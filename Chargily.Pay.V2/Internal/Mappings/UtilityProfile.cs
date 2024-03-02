using AutoMapper;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class UtilityProfile : Profile
{
  public UtilityProfile()
  {
    var languages = new Dictionary<LocaleType, string>()
                    {
                      { LocaleType.Arabic, "ar" },
                      { LocaleType.English, "en" },
                      { LocaleType.French, "fr" },
                    };
    var locales = new Dictionary<string, LocaleType>()
                  {
                    { "ar", LocaleType.Arabic },
                    { "en", LocaleType.English },
                    { "fr", LocaleType.French },
                  };
    
    CreateMap<string, Currency>()
     .ConstructUsing((x, _) => Enum.Parse<Currency>(x.ToUpper()))
     .ReverseMap()
     .ConstructUsing((x, _) => Enum.GetName(x)!.ToLowerInvariant());

    CreateMap<string, LocaleType>()
     .ConstructUsing((x, _) => locales[x])
     .ReverseMap()
     .ConstructUsing((x, _) => languages[x]);

    CreateMap<string, Country>()
     .ConstructUsing((x, _) => CountryCode.GetCountry(x) ?? Country.Algeria)
     .ReverseMap()
     .ConstructUsing((x, _) => CountryCode.GetCountryCode(x) ?? "DZ");
  }
}