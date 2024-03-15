namespace Chargily.Pay.Models;

public sealed record Product
{
  public string Id { get; set; }
  public string Name { get; init; }
  public string? Description { get; init; }
  public List<Uri>? ImagesUrls { get; init; } = new();
  public List<string>? Metadata { get; init; } = new();
  public IReadOnlyList<ProductPrice> Prices { get; internal init; } = [];
}