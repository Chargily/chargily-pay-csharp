namespace Chargily.Pay.Models;

public sealed record UpdatePaymentLink
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public bool? IsActive { get; init; }
    public string? CompletionMessage { get; init; }
    public LocaleType? Language { get; init; }
    public bool? PassFeesToCustomer { get; init; }
    public List<string>? Metadata { get; init; } = new();
    
    // public string? ShippingAddress { get; init; }
    public bool? CollectShippingAddress { get; init; }
    
    public IReadOnlyList<PaymentLinkPriceItem>? Items { get; init; }

}