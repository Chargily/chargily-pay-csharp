namespace Chargily.Pay.V2.Models;

internal sealed record DeleteResult<T>
{
    public bool IsSuccessful { get; init; }
    public T? DeletedValue { get; init; }
}