namespace OrderMate.Web.v1.Products.Create;

public sealed class CreateProductRequest
{
  public const string Route = "/api/products";

  public required string Name { get; init; }

  public decimal Price { get; init; }

  public int Stock { get; init; }

  public int CategoryId { get; init; }
}
