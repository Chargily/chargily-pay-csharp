namespace Chargily.Pay.V2.Models;

public sealed record PaymentLinkResponse : PaymentLink
{
    public Uri Url { get; init; }
    public IReadOnlyList<PaymentLinkItem> Items { get; init; } = [];
}