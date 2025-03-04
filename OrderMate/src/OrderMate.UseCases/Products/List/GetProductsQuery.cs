
using OrderMate.Core.Aggregates.ProductAggregate;

namespace OrderMate.UseCases.Products.List;

public sealed record GetProductsQuery() : IQuery<Result<IEnumerable<ProductDto>>>;

public sealed class GetProductsQueryHandler(IReadRepository<Product> productRepository) : IQueryHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
{
  public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
  {
    var products = productRepository
  }
}
