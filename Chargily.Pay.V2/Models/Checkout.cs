namespace Chargily.Pay.V2.Models;

public sealed record Checkout
{
    /// <summary>
    /// Checkout must provide either Checkout Items or Amount & Currency
    /// </summary>
    /// <param name="checkoutItems">Checkout Items</param>
    public Checkout(CheckoutItem[] checkoutItems)
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
    public CheckoutItem[]? Items { get; private set; }
    public decimal? Amount { get; private set; }
    public Currency? Currency { get; private set; }
    public Customer? Customer { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public Uri? OnSuccessRedirectUrl { get; init; }
    public Uri? OnFailureRedirectUrl { get; init; }
    public Uri? WebhookEndpointUrl { get; init; }
    public string Description { get; init; }
    public LocaleType Language { get; init; }
    public bool PassFeesToCustomer { get; init; }
    public List<object>? Metadata { get; init; } = new();
}