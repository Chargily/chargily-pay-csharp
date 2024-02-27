using System.Text.Json.Serialization;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Responses;

internal record BaseObjectResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }
    
    [JsonPropertyName("entity")]
    public EntityType EntityType { get; init; }
    
    [JsonPropertyName("livemode")]
    public bool IsLiveMode { get; init; }
    
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }
    
    [JsonPropertyName("updated_at")]
    public DateTimeOffset LastUpdatedAt { get; init; }
}