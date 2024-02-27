namespace Chargily.Pay.V2.Models;

public sealed record Product
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public List<Uri> ImagesUrls { get; init; } = new();
    public List<object> Metadata { get; init; } = new();
}