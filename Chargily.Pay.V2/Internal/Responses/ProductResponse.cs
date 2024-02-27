namespace Chargily.Pay.V2.Internal.Responses;

internal record ProductResponse : BaseObjectResponse
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string[] ImagesUrls { get; init; }
    public List<object>? Metadata { get; init; } = new();
}