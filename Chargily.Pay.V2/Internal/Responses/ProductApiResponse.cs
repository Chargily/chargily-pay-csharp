namespace Chargily.Pay.V2.Internal.Responses;

internal record ProductApiResponse : BaseObjectApiResponse
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string[]? ImagesUrls { get; init; }
    public List<string>? Metadata { get; init; } = new();
}