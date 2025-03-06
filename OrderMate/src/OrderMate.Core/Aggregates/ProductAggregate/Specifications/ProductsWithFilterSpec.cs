using OrderMate.Core.Aggregates.ProductAggregate.Filters;
using OrderMate.Core.Aggregates.ProductAggregate.Specifications.Extensions;

namespace OrderMate.Core.Aggregates.ProductAggregate.Specifications;

public class ProductsWithFilterSpec : Specification<Product>
{
  public ProductsWithFilterSpec(ProductFilter filter)
  {
    Query.ApplyFilters(filter)
         .ApplyOrdering(filter)
         .ApplyPaging(filter);
  }
}
