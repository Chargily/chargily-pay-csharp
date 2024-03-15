using System.Text.Json.Serialization;

namespace Chargily.Pay.Internal.Responses;

internal record CheckoutItemApiResponse : BaseObjectApiResponse
{
    [JsonPropertyName("product_id")] public string ProductId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = null!;
    public int Quantity { get; init; }
    public List<string>? Metadata { get; init; } = new();
}