namespace Chargily.Pay.Models;

public record CustomerAddress
{
  public Country Country { get; init; }
  public string State { get; init; }
  public string Address { get; init; }
};