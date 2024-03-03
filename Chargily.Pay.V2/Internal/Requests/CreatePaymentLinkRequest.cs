using System.Text.Json.Serialization;

namespace Chargily.Pay.V2.Internal.Requests;

internal record CreatePaymentLinkRequest
{
    public string Name { get; init; }
    [JsonPropertyName("active")] public bool IsActive { get; init; }

    [JsonPropertyName("after_completion_message")]
    public string CompletionMessage { get; init; }

    [JsonPropertyName("locale")] public string Language { get; init; }

    [JsonPropertyName("pass_fees_to_customer")]
    public bool PassFeesToCustomer { get; init; }

    public List<string>? Metadata { get; init; } = new();
}