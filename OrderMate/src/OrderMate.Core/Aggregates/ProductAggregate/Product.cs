using OrderMate.Core.Aggregates.ProductAggregate.Enums;

namespace OrderMate.Core.Aggregates.ProductAggregate;

public class Product : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = default!;
  public decimal Price { get; private set; } = default!;
  public int Stock { get; private set; }
  public ProductCategory Category { get; private set; } = default!;

  private Product() { }

  public Product(string name, decimal price, int stock, ProductCategory category)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Price = Guard.Against.NegativeOrZero(price, nameof(price));
    Stock = Guard.Against.Negative(stock, nameof(stock));
    Category = Guard.Against.Null(category, nameof(category));
  }

  public void UpdateStock(int quantity)
  {
    Stock = Guard.Against.Negative(Stock - quantity, nameof(quantity));
  }

  /// <summary>
  /// Update of product name, price and category information
  /// </summary>
  /// <param name="name"></param>
  /// <param name="price"></param>
  /// <param name="categoryId"></param>
  public void UpdateProduct(string name, decimal price, int categoryId)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Price = Guard.Against.NegativeOrZero(price, nameof(price));
    Category = Guard.Against.Null(ProductCategory.FromValue(categoryId), nameof(categoryId));
  }

  /// <summary>
  /// Set the new stock
  /// </summary>
  /// <param name="stock"></param>
  public void SetStock(int stock)
  {
    Stock = Guard.Against.Negative(stock, nameof(stock));
  }
}
