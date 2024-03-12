namespace Chargily.Pay.V2.Models;

public sealed record UpdatePrice
{
    public string Id { get; init; }
    public decimal? Amount { get; init; }
    public Currency? Currency { get; init; }
    public List<string>? Metadata { get; init; }
    public string ProductId { get; init; }
}