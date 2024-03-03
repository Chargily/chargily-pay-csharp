using Chargily.Pay.V2.Internal.Responses;

namespace Chargily.Pay.V2.Models;

public sealed record PagedResponse<T>
{
    public IReadOnlyList<T> Data { get; init; }
    public int CurrentPage { get; init; }
    public int? PreviousPage { get; init; }
    public int? NextPage { get; init; }
    public int FirstPage { get; init; }
    public int? LastPage { get; init; }
    public int PageSize { get; init; }
    public int Total { get; init; }
}