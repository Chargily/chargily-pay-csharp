using System.Text.Json.Serialization;
using Chargily.Pay.Models;

namespace Chargily.Pay.Internal.Responses;

internal record BalanceResponse
{
    [JsonPropertyName("entity")] public EntityType EntityType { get; init; }

    [JsonPropertyName("livemode")] public bool IsLiveMode { get; init; }

    public WalletApiResponse[] Wallets { get; init; }
}