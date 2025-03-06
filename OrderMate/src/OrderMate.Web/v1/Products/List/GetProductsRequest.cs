namespace OrderMate.Web.v1.Products.List;

public class GetProductsRequest
{
  public const string Route = "/api/products";

  public int Page { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? SortBy { get; set; }
  public string? OrderBy { get; set; } = "asc";
  public string? Category { get; set; }
  public decimal? PriceFrom { get; set; }
  public decimal? PriceTo { get; set; }
}
