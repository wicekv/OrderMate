namespace OrderMate.Core.Aggregates.UserAggregate.Filters;

public sealed record CustomFilter : BaseFilter
{
  public string? Role { get; init; }
}
