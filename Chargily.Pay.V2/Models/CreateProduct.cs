namespace Chargily.Pay.V2.Models;

public sealed record CreateProduct
{
  public string Name { get; init; }
  public string? Description { get; init; }
  public List<Uri> ImagesUrls { get; init; } = new();
  public List<string>? Metadata { get; init; } = new();
}