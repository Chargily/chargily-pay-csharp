namespace Chargily.Pay.V2.Models;

public record Wallet
{
    public Currency Currency { get; init; }
    public decimal Balance { get; init; }
    public decimal ReadyForPayout { get; init; }
    public decimal OnHold { get; init; }
}