using System.Text.Json.Serialization;

namespace Chargily.Pay.V2.Internal.Responses;

internal record PriceApiResponse : BaseObjectApiResponse
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = null!;
    public List<string>? Metadata { get; init; } = new();
    [JsonPropertyName("product_id")]
    public string ProductId { get; init; }
}