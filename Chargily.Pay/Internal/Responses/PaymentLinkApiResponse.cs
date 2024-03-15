using System.Text.Json.Serialization;
using Chargily.Pay.Internal.JsonConverters;

namespace Chargily.Pay.Internal.Responses;

internal record PaymentLinkApiResponse : BaseObjectApiResponse
{
    public string Name { get; init; }
    [JsonPropertyName("active")] public bool IsActive { get; init; }

    [JsonPropertyName("after_completion_message")]
    public string CompletionMessage { get; init; }

    [JsonPropertyName("locale")] public string Language { get; init; }

    [JsonPropertyName("pass_fees_to_customer")]
    public bool PassFeesToCustomer { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }

    public List<string>? Metadata { get; init; } = new();
    
    // [JsonPropertyName("shipping_address")]
    // public string? ShippingAddress { get; init; }
    
    [JsonPropertyName("collect_shipping_address")]
    [JsonConverter(typeof(IntegerToBooleanConverter))]
    public bool CollectShippingAddress { get; init; }

};