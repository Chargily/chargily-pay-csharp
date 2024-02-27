using System.Text.Json.Serialization;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Responses;

internal record BalanceResponse
{
    [JsonPropertyName("entity")] public EntityType EntityType { get; init; }

    [JsonPropertyName("livemode")] public bool IsLiveMode { get; init; }

    public WalletResponse[] Wallets { get; init; }
}