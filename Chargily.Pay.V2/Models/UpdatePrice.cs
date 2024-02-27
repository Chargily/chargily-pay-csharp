namespace Chargily.Pay.V2.Models;

internal sealed record UpdatePrice
{
    public string Id { get; init; }
    public decimal? Amount { get; init; }
    public Currency? Currency { get; init; }
    public List<object>? Metadata { get; init; }
}