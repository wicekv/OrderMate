using OrderMate.Core.Aggregates.UserAggregate.Enums;
using OrderMate.Core.Aggregates.Users;
using OrderMate.UnitTests.TestHelpers.Fakes;

namespace OrderMate.UnitTests.Core.Aggregates.UserAggregate;

public sealed class UserTests
{
  [Fact]
  public void CreateUser_WhenDataIsValid_ShouldCreateUser()
  {
    //Arrange
    var name = "Wicek";
    var email = "wicek@wicek.com";
    var role = UserRole.Customer;

    //Act
    var user = new User(name, email, role);

    //Assert
    user.Name.Should().Be(name);
    user.Email.Should().Be(email);
    user.Role.Should().Be(role);
    user.Orders.Should().BeEmpty();
  }

  [Fact]
  public void PlaceOrder_WhenOrderIsValid_ShouldAddOrder()
  {
    //Arrange
    var user = FakeUserFactory.CreateUser();
    var order = FakeOrderFactory.CreateOrder(user.Id);

    //Act
    user.PlaceOrder(order);

    //Assert
    user.Orders.Should().HaveCount(1);
    user.Orders.First().Should().Be(order);
  }

  [Fact]
  public void PlaceOrder_WhenMultipleOrdersAdded_ShouldStoreAllOrders()
  {
    // Arrange
    var user = FakeUserFactory.CreateUser();
    var order1 = FakeOrderFactory.CreateOrder(user.Id);
    var order2 = FakeOrderFactory.CreateOrder(user.Id);

    // Act
    user.PlaceOrder(order1);
    user.PlaceOrder(order2);

    // Assert
    user.Orders.Should().HaveCount(2);
    user.Orders.Should().Contain(order1);
    user.Orders.Should().Contain(order2);
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  public void CreateUser_WhenNameIsNullOrEmpty_ShouldThrowArgumentException(string invalidName)
  {

    //Act
    var action = () => new User(invalidName, "wicek@wicek.com", UserRole.Customer);

    //Assert
    var exception = Assert.Throws<ArgumentException>(action);
    exception.Should().NotBeNull();
  }

  [Theory]
  [InlineData("")]
  [InlineData(" ")]
  public void CreateUser_WhenEmailIsNullOrEmpty_ShouldThrowArgumentException(string invalidEmail)
  {
    // Act
    var action = () => new User("wicek", invalidEmail, UserRole.Customer);

    // Assert
    var exception = Assert.Throws<ArgumentException>(action);
    exception.Should().NotBeNull();
  }
}
