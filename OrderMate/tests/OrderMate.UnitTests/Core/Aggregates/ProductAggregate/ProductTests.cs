using OrderMate.UnitTests.TestHelpers.Fakes;

namespace OrderMate.UnitTests.Core.Aggregates.ProductAggregate;

public sealed class ProductTests
{
  [Fact]
  public void CreateProduct_WhenDataIsValid_ShouldCreateProduct()
  {
    // Act
    var product = FakeProductFactory.CreateProduct();

    // Assert
    product.Name.Should().NotBeNullOrWhiteSpace();
    product.Price.Should().BeGreaterThan(0);
    product.Stock.Should().BeGreaterThanOrEqualTo(0);
    product.Category.Should().NotBeNull();
  }

  [Fact]
  public void UpdateStock_WhenQuantityIsValid_ShouldDecreaseStockCorrectly()
  {
    //Arrange
    var initialStock = 20;
    var quantityToRemove = 7;

    var product = FakeProductFactory.CreateProduct(stock: initialStock);

    //Act
    product.UpdateStock(quantityToRemove);

    //Assert
    product.Stock.Should().Be(initialStock - quantityToRemove);
  }

  [Fact]
  public void UpdateStock_WhenQuantityExceedStock_ShouldThrowArgumentException()
  {
    //Arrange 
    var product = FakeProductFactory.CreateProduct(stock: 3);

    //Act
    var action = () => product.UpdateStock(5);

    //Assert
    var exception = Assert.Throws<ArgumentException>(action);
    exception.Should().NotBeNull();
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  public void UpdateStock_WhenQuantityIsZeroOrNegative_ShouldThrowArgumentException(int invalidQuantity)
  {
    // Arrange
    var product = FakeProductFactory.CreateProduct(stock: 10);

    // Act
    var action = () => product.UpdateStock(invalidQuantity);

    // Assert
    var exception = Assert.Throws<ArgumentException>(action);
    exception.Should().NotBeNull();
  }
}
