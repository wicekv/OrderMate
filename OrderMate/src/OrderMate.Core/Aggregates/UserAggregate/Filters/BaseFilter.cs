namespace OrderMate.Core.Aggregates.UserAggregate.Filters;

public record BaseFilter
{
  public int? Page { get; init; }
  public int? PageSize { get; init; }
  public string? SortBy { get; init; }
  public string? OrderBy { get; init; }
}
