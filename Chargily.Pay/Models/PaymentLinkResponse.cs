namespace Chargily.Pay.Models;

public sealed record PaymentLinkResponse : PaymentLink
{
    public Uri? Url { get; init; }
    public IReadOnlyList<PaymentLinkItem> Items { get; init; } = [];
    

}