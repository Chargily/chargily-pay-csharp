using System.Text.Json.Serialization;

namespace Chargily.Pay.V2.Internal.Responses;

internal record PaymentLinkItemApiResponse : BaseObjectApiResponse
{
    [JsonPropertyName("product_id")] public string ProductId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = null!;
    public int Quantity { get; init; }

    [JsonPropertyName("adjustable_quantity")]
    public bool AdjustableQuantity { get; init; }

    public List<object>? Metadata { get; init; } = new();
}