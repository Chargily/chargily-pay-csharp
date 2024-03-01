using System.Text.Json.Serialization;

namespace Chargily.Pay.V2.Models;

public enum EntityType
{
    [JsonPropertyName("product")]
    Product,
    [JsonPropertyName("customer")]
    Customer,
    [JsonPropertyName("price")]
    Price,
    [JsonPropertyName("checkout")]
    Checkout,
    [JsonPropertyName("payment_link")]
    PaymentLink,
    [JsonPropertyName("balance")]
    Balance,
    PaymentLinkItem,
    CheckoutItem
}