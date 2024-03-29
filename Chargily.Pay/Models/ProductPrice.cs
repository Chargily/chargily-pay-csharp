﻿namespace Chargily.Pay.Models;

public record ProductPrice
{
  public string Id { get; set; }
  public decimal Amount { get; init; }
  public Currency Currency { get; init; }
  public List<string>? Metadata { get; init; }
}