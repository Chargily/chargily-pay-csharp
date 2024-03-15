namespace Chargily.Pay.Models;

internal sealed record DeleteResult<T>
{
    public bool IsSuccessful { get; init; }
    public T? DeletedValue { get; init; }
}