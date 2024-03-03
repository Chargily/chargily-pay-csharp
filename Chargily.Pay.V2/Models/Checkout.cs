namespace Chargily.Pay.V2.Models;

public sealed record Checkout
{
    internal Checkout()
    {
        
    }
    
    /// <summary>
    /// Checkout must provide either Checkout Items or Amount & Currency
    /// </summary>
    /// <param name="checkoutItems">Checkout Items</param>
    public Checkout(List<CreateCheckoutItem> checkoutItems)
    {
        Items = checkoutItems;
    }

    /// <summary>
    /// Checkout must provide either Checkout Items or Amount & Currency
    /// </summary>
    /// <param name="amount">Checkout Amount</param>
    /// <param name="currency">Currency</param>
    public Checkout(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }
    public IReadOnlyList<CreateCheckoutItem>? Items { get; internal set; }
    public decimal? Amount { get; internal set; }
    public Currency? Currency { get; internal set; }
    public string? CustomerId { get; init; }

    public PaymentMethod PaymentMethod { get; init; }
    public Uri? OnSuccessRedirectUrl { get; init; }
    public Uri? OnFailureRedirectUrl { get; init; }
    public Uri? WebhookEndpointUrl { get; init; }
    public string Description { get; init; }
    public LocaleType Language { get; init; }
    public bool PassFeesToCustomer { get; init; }
    public List<string>? Metadata { get; init; } = new();
}