using OrderMate.Core.Aggregates.ProductAggregate.Filters;
using OrderMate.Core.Aggregates.UserAggregate.Filters;

namespace OrderMate.Core.Aggregates.ProductAggregate.Specifications.Extensions;

public static class ProductSpecExtensions
{
  public static ISpecificationBuilder<Product> ApplyOrdering(this ISpecificationBuilder<Product> builder, BaseFilter? filter = null)
  {
    if (filter is null) return builder.OrderBy(x => x.Id);

    var isAscending = !(filter.OrderBy?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false);

    return filter.SortBy switch
    {
      nameof(Product.Name) => isAscending ? builder.OrderBy(x => x.Name) : builder.OrderByDescending(x => x.Name),
      nameof(Product.Price) => isAscending ? builder.OrderBy(x => x.Price) : builder.OrderByDescending(x => x.Price),
      _ => builder.OrderBy(x => x.Id)
    };
  }

  public static ISpecificationBuilder<Product> ApplyPaging(this ISpecificationBuilder<Product> builder, BaseFilter filter)
  {
    if (filter.Page.HasValue && filter.PageSize.HasValue)
    {
      int skip = (filter.Page.Value - 1) * filter.PageSize.Value;
      builder.Skip(skip).Take(filter.PageSize.Value);
    }
    return builder;
  }

  public static ISpecificationBuilder<Product> ApplyFilters(this ISpecificationBuilder<Product> builder, ProductFilter filter)
  {
    if (!string.IsNullOrWhiteSpace(filter.Category))
    {
      builder.Where(p => p.Category.Name == filter.Category);
    }

    if (filter.PriceFrom.HasValue)
    {
      builder.Where(p => p.Price >= filter.PriceFrom.Value);
    }

    if (filter.PriceTo.HasValue)
    {
      builder.Where(p => p.Price <= filter.PriceTo.Value);
    }

    return builder;
  }
}
