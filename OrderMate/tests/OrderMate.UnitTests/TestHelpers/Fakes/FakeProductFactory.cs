using OrderMate.Core.Aggregates.ProductAggregate.Enums;
using OrderMate.Core.Aggregates.ProductAggregate;
using Bogus;

namespace OrderMate.UnitTests.TestHelpers.Fakes;

public static class FakeProductFactory
{
  private static readonly Faker _faker = new();

  private static readonly ProductCategory[] _categories =
  {
        ProductCategory.Electronics,
        ProductCategory.Clothing,
        ProductCategory.HomeAppliances
    };

  public static Product CreateProduct(
      int? id = null,
      string? name = null,
      decimal? price = null,
      int? stock = null,
      ProductCategory? category = null)
  {
    var product = new Product(
        name ?? _faker.Commerce.ProductName(),
        price ?? _faker.Random.Decimal(10, 5000),
        stock ?? _faker.Random.Int(0, 100),
        category ?? _faker.PickRandom(_categories)
    );

    if (id.HasValue)
    {
      typeof(Product).GetProperty(nameof(Product.Id))!.SetValue(product, id.Value);
    }
    else
    {
      typeof(Product).GetProperty(nameof(Product.Id))!.SetValue(product, _faker.Random.Int(1, 1000));
    }

    return product;
  }
}
