using System.Text.Json.Serialization;
using Chargily.Pay.V2.Internal.JsonConverters;

namespace Chargily.Pay.V2.Internal.Responses;

internal record CheckoutApiResponse : BaseObjectApiResponse
{
    public decimal? Amount { get; init; }
    public string? Currency { get; init; }
    [JsonPropertyName("customer_id")] public string? CustomerId { get; init; }
    [JsonPropertyName("payment_method")] public string PaymentMethod { get; init; }
    [JsonPropertyName("success_url")] public string OnSuccessRedirectUrl { get; init; }
    [JsonPropertyName("failure_url")] public string OnFailureRedirectUrl { get; init; }
    [JsonPropertyName("webhook_endpoint")] public string? WebhookEndpointUrl { get; init; }
    public string? Description { get; init; }
    [JsonPropertyName("locale")] public string Language { get; init; }

    [JsonPropertyName("pass_fees_to_customer")]
    [JsonConverter(typeof(IntegerToBooleanConverter))]
    public bool PassFeesToCustomer { get; init; }

    public List<string>? Metadata { get; init; } = new();

    public string Status { get; init; }
    public decimal Fees { get; init; }
    [JsonPropertyName("payment_link_id")] public string? PaymentLinkId { get; init; }
    [JsonPropertyName("invoice_id")] public string InvoiceId { get; init; }
    [JsonPropertyName("checkout_url")] public string CheckoutUrl { get; init; }
};