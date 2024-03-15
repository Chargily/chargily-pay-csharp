using AutoMapper;
using Chargily.Pay.Models;

namespace Chargily.Pay.Internal.Mappings;

public class UtilityProfile : Profile
{
  public UtilityProfile()
  {
    CreateMap<long, DateTimeOffset>()
     .ConstructUsing((x, _) => DateTimeOffset.FromUnixTimeSeconds(x))
     .ReverseMap()
     .ConstructUsing((x, _) => x.ToUnixTimeSeconds());
    
    CreateMap<string, Currency>()
     .ConstructUsing((x, _) => Enum.Parse<Currency>(x.ToUpper()))
     .ReverseMap()
     .ConstructUsing((x, _) => Enum.GetName(x)!.ToLower());

    CreateMap<string, LocaleType>()
     .ConstructUsing((x, _) => Language.GetLocalType(x) ?? LocaleType.English)
     .ReverseMap()
     .ConstructUsing((x, _) => Language.GetLanguage(x) ?? "en");

    CreateMap<string, Country>()
     .ConstructUsing((x, _) => CountryCode.GetCountry(x) ?? Country.Algeria)
     .ReverseMap()
     .ConstructUsing((x, _) => CountryCode.GetCountryCode(x) ?? "DZ");
  }
}