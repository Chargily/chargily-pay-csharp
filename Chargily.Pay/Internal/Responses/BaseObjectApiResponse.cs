using System.Text.Json.Serialization;

namespace Chargily.Pay.Internal.Responses;

internal record BaseObjectApiResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; }
    
    [JsonPropertyName("entity")]
    public string EntityType { get; init; }
    
    [JsonPropertyName("livemode")]
    public bool IsLiveMode { get; init; }
    
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; init; }
    
    [JsonPropertyName("updated_at")]
    public long LastUpdatedAt { get; init; }
}