using System.Text.Json.Serialization;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Responses;

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