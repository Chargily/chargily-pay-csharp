namespace Chargily.Pay.V2.Models;

public record PaymentLink
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public string CompletionMessage { get; init; }
    public LocaleType Language { get; init; }
    public bool PassFeesToCustomer { get; init; }
    public List<object>? Metadata { get; init; } = new();
}