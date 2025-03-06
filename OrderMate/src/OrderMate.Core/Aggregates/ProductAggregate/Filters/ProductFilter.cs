using OrderMate.Core.Aggregates.UserAggregate.Filters;

namespace OrderMate.Core.Aggregates.ProductAggregate.Filters;
public record ProductFilter : BaseFilter
{
  public string? Category { get; init; }
  public decimal? PriceFrom { get; init; }
  public decimal? PriceTo { get; init; }
}
