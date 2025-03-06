using OrderMate.Core.Aggregates.ProductAggregate.Filters;
using OrderMate.UseCases.Products;
using OrderMate.UseCases.Products.List;

namespace OrderMate.Web.v1.Products.List;

public class GetProductsEndpoint(IMediator _mediator) : Endpoint<GetProductsRequest, PagedResult<List<ProductDto>>>
{
  public override void Configure()
  {
    Get(GetProductsRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Get products lists with pagination";
      s.Description = "returns the pagination list with products";
      s.ExampleRequest = new GetProductsRequest();
    });
  }

  public override async Task HandleAsync(GetProductsRequest request, CancellationToken cancellationToken)
  {
    var filter = new ProductFilter
    {
      Page = request.Page,
      PageSize = request.PageSize,
      SortBy = request.SortBy,
      OrderBy = request.OrderBy,
      Category = request.Category,
      PriceFrom = request.PriceFrom,
      PriceTo = request.PriceTo
    };

    var result = await _mediator.Send(new GetProductsQuery(filter));

    if (result.IsSuccess)
    {

      Response = result;
      return;
    }
  }
}
