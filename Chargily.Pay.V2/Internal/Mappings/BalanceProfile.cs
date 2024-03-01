using AutoMapper;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class BalanceProfile : Profile
{
  public BalanceProfile()
  {
    CreateMap<WalletApiResponse, Wallet>()
     .ConstructUsing((res, ctx) => new Wallet()
                                   {
                                     Currency = ctx.Mapper.Map<Currency>(res.Currency),
                                     Balance = res.Balance,
                                     OnHold = res.OnHold,
                                     ReadyForPayout = Convert.ToDecimal(res.ReadyForPayout)
                                   });

    CreateMap<BalanceResponse, IReadOnlyList<Wallet>>()
     .ConstructUsing((res, ctx) => res.Wallets.Select(ctx.Mapper.Map<Wallet>).ToList());
  }
}