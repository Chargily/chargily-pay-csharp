using System.Text.Json.Serialization;

namespace Chargily.Pay.V2.Internal.Responses;

internal record WalletApiResponse
{
    public string Currency { get; init; }
    public decimal Balance { get; init; }
    [JsonPropertyName("ready_for_payout")] public string ReadyForPayout { get; init; }
    [JsonPropertyName("on_hold")] public decimal OnHold { get; init; }
}