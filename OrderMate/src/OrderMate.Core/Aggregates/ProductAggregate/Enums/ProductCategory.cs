namespace OrderMate.Core.Aggregates.ProductAggregate.Enums;

public class ProductCategory : SmartEnum<ProductCategory>
{
  public static readonly ProductCategory Electronics = new(nameof(Electronics), 1);
  public static readonly ProductCategory Clothing = new(nameof(Clothing), 2);
  public static readonly ProductCategory HomeAppliances = new(nameof(HomeAppliances), 3);

  private ProductCategory(string name, int value) : base(name, value) { }
}
