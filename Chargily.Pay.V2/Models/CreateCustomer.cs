namespace Chargily.Pay.V2.Models;

public sealed record CreateCustomer
{
  public string Name { get; init; }
  public string? Email { get; init; }
  public string? Phone { get; init; }
  public CustomerAddress? Address { get; init; }
  public List<string>? Metadata { get; init; } = new();
}