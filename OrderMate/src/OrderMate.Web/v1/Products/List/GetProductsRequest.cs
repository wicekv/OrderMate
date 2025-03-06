namespace OrderMate.Web.v1.Products.List;

public class GetProductsRequest
{
  public const string Route = "/api/products";

  public int? Page { get; init; }
  public int? PageSize { get; init; }
  public string? SortBy { get; init; }
  public string? OrderBy { get; init; }
  public string? Category { get; init; }
  public decimal? PriceFrom { get; init; }
  public decimal? PriceTo { get; init; }
}
