namespace OrderMate.Core.Aggregates.ProductAggregate.Specifications;

public class ProductsByIdsSpec : Specification<Product>
{
  public ProductsByIdsSpec(List<int> productIds)
  {
    Query.Where(p => productIds.Contains(p.Id));
  }
}
