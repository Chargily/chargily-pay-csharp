namespace Chargily.Pay.V2.Models;

internal record PaymentLink
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public string CompletionMessage { get; init; }
    public LocaleType Language { get; init; }
    public bool PassFeesToCustomer { get; init; }
    public List<object> Metadata { get; init; } = new();
}

internal sealed record PaymentLinkResponse : PaymentLink
{
    public Uri Url { get; init; }
}
