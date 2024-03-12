namespace Chargily.Pay.V2.Models;

public sealed record Response<T>
{
  public string Id { get; init; }
  public bool IsLiveMode { get; init; }
  public DateTimeOffset CreatedAt { get; init; }
  public DateTimeOffset LastUpdatedAt { get; init; }
  public T Value { get; init; }
}