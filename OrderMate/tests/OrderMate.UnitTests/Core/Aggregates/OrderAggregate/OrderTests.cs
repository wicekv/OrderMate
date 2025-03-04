using OrderMate.Core.Aggregates.OrderAggregate;
using OrderMate.Core.Aggregates.OrderAggregate.Enums;
using OrderMate.UnitTests.TestHelpers.Fakes;

namespace OrderMate.UnitTests.Core.Aggregates.OrderAggregate;

public sealed class OrderTests
{
  [Fact]
  public void CreateOrder_WhenUserIdIsValid_ShouldCreateOrderWithNewStatus()
  {
    //Arrange
    var userId = 1;

    //Act
    var order = new Order(userId);

    //Assert
    order.UserId.Should().Be(userId);
    order.Status.Should().Be(OrderStatus.New);
    order.OrderItems.Should().BeEmpty();
  }

  [Fact]
  public void AddProduct_WhenProductIsValid_ShouldAddOrderItem()
  {
    //Arrange
    var order = new Order(1);
    var product = FakeProductFactory.CreateProduct();
    var quantity = 2;

    //Act
    order.AddProduct(product, quantity);

    //Assert
    order.OrderItems.Should().HaveCount(1);
    var orderItem = order.OrderItems.First();
    orderItem.ProductId.Should().Be(product.Id);
    orderItem.Quantity.Should().Be(quantity);
    orderItem.UnitPrice.Should().Be(product.Price);
  }

  [Fact]
  public void AddProduct_WhenProductAlreadyExist_ShouldIncreaseQuantity()
  {
    //Arrange
    var order = new Order(1);
    var product = FakeProductFactory.CreateProduct();
    var quantity = 1;
    order.AddProduct(product, quantity);

    //Act
    order.AddProduct(product, quantity);

    //Assert
    order.OrderItems.Should().HaveCount(1);
    order.OrderItems.First().Quantity.Should().Be(2);
  }

  [Fact]
  public void IncreaseQuantity_WhenValidAmount_ShouldIncreaseQuantity()
  {
    // Arrange
    var orderItem = new OrderItem(1, 2, 50m);

    // Act
    orderItem.IncreaseQuantity(3);

    // Assert
    orderItem.Quantity.Should().Be(5);
  }

  [Fact]
  public void ChangeStatus_WhenStatusIsValid_ShouldUpdateStatus()
  {
    //Act
    var order = new Order(1);

    //Act
    order.ChangeStatus(OrderStatus.InProgress);

    //Assert
    order.Status.Should().Be(OrderStatus.InProgress);
  }

  [Fact]
  public void GetTotalPrice_WhenOrderHasItems_ShouldReturnCorrectTotalPrice()
  {
    //Arrange
    var order = new Order(1);

    var product1 = FakeProductFactory.CreateProduct(id: 1, price: 100m);
    var product2 = FakeProductFactory.CreateProduct(id: 2, price: 50m);

    order.AddProduct(product1, 2);
    order.AddProduct(product2, 3);

    //Act
    var totalPrice = order.GetTotalPrice();

    //Assert
    totalPrice.Should().Be((2 * 100m) + (3 * 50m));
  }
}
