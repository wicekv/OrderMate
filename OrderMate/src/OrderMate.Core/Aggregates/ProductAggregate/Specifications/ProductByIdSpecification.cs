namespace OrderMate.Core.Aggregates.ProductAggregate.Specifications;

public class ProductByIdSpecification : Specification<Product>
{
  public ProductByIdSpecification(int productId)
  {
    Query.Where(p => p.Id == productId);
  }
}
