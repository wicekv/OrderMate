using OrderMate.Core.Aggregates.ProductAggregate;
using OrderMate.Core.Aggregates.ProductAggregate.Filters;
using OrderMate.Core.Aggregates.ProductAggregate.Specifications;

namespace OrderMate.UseCases.Products.List;

public sealed record GetProductsQuery(ProductFilter filter) : IQuery<PagedResult<List<ProductDto>>>;

public sealed class GetProductsQueryHandler(IReadRepository<Product> productRepository) : IQueryHandler<GetProductsQuery, PagedResult<List<ProductDto>>>
{
  public async Task<PagedResult<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
  {
    var totalCount = await productRepository.CountAsync();

    var spec = new ProductsWithFilterSpec(request.filter);
    var products = await productRepository.ListAsync(spec, cancellationToken);

    var productDtos = products
            .Select(p => new ProductDto(
                p.Name,
                p.Price,
                p.Stock,
                p.Category.Name))
            .ToList();

    var pageSize = request.filter.PageSize ?? totalCount;
    var page = request.filter.Page ?? 1;

    var pagedInfo = new PagedInfo(page, pageSize, (long)Math.Ceiling((double)totalCount / pageSize), totalCount);

    return new PagedResult<List<ProductDto>>(pagedInfo, productDtos);
  }
}
