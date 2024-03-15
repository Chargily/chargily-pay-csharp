namespace Chargily.Pay.Models;

public sealed record UpdateProduct
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public List<Uri>? ImagesUrls { get; init; } = new();
    public List<string>? Metadata { get; init; } = new();
}