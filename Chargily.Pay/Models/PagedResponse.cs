namespace Chargily.Pay.Models;

public sealed record PagedResponse<T>
{
    public IReadOnlyList<T> Data { get; init; }
    public int CurrentPage { get; init; }
    public int? PreviousPage { get; init; }
    public int? NextPage { get; init; }
    public int FirstPage { get; init; }
    public int LastPage { get; init; }
    public int PageSize { get; init; }
    public int Total { get; init; }
    public bool HasNextPage => NextPage is not null && NextPage <= TotalPages;
    public bool HasPreviousPage => PreviousPage is not null || CurrentPage > FirstPage;
    public int TotalPages => Total is 0 ? 1 : Convert.ToInt32(Math.Truncate(Total * 1d / PageSize) + ((Total % PageSize) > 0 ? 1 : 0));
}