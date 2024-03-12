using Chargily.Pay.V2.Internal.Responses;
using Refit;

namespace Chargily.Pay.V2.Internal.Endpoints;

internal partial interface IChargilyPayApi
{
    [Get("/balance")]
    Task<BalanceResponse> GetBalance();
}