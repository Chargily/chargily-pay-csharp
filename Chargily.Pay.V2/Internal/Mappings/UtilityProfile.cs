using AutoMapper;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class UtilityProfile : Profile
{
  public UtilityProfile()
  {
    CreateMap<string, Currency>()
     .ConstructUsing((x, _) => Enum.Parse<Currency>(x))
     .ReverseMap()
     .ConstructUsing((x, _) => Enum.GetName(x)!);

    CreateMap<string, LocaleType>()
     .ConstructUsing((x, _) => Enum.Parse<LocaleType>(x));

    CreateMap<string, Country>()
     .ConstructUsing((x, _) => Enum.Parse<Country>(x));
  }
}