﻿using System.Text.Json.Serialization;

namespace Chargily.Pay.Internal.Responses;

internal record PagedApiResponse<TData> where TData : BaseObjectApiResponse
{
  public List<TData> Data { get; init; } = [];
  public int Total { get; init; }

  [JsonPropertyName("path")] public string PathUrl { get; init; } = null!;

  [JsonPropertyName("first_page_url")] public string FirstPageUrl { get; init; } = null!;

  /// Omitted because of Api bug.
  //[JsonPropertyName("last_page_url")] public string? LastPageUrl { get; init; } = null;
  [JsonPropertyName("next_page_url")]
  public string? NextPageUrl { get; init; } = null;

  [JsonPropertyName("prev_page_url")] public string? PreviousPageUrl { get; init; } = null;

  [JsonPropertyName("current_page")] public int CurrentPage { get; init; }
  [JsonPropertyName("last_page")] public int LastPage { get; init; }
  [JsonPropertyName("per_page")] public int PageSize { get; init; }

  internal int? GetNextPage()
  {
    return Uri.TryCreate(NextPageUrl, UriKind.Absolute, out var url)
             ? url.GetPage()!
             : null;
  }

  internal int GetTotalPages()
  {
    return Total is 0 ? 1 : Convert.ToInt32(Math.Truncate(Total * 1d / PageSize) + ((Total % PageSize) > 0 ? 1 : 0));
  }
}