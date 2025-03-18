using OrderMate.Core.Aggregates.OrderAggregate;
using OrderMate.Core.Aggregates.UserAggregate.Enums;
using OrderMate.Core.Aggregates.UserAggregate.ErrorMessages;

namespace OrderMate.Core.Aggregates.Users;

public class User : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = default!;
  public string Email { get; private set; } = default!;
  public UserRole Role { get; private set; } = default!;

  private readonly List<Order> _orders = new();
  public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

  private User() { }

  public User(string name, string email, UserRole role)
  {
    Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    Email = Guard.Against.NullOrWhiteSpace(email, nameof(email));
    Role = Guard.Against.Null(role, nameof(role));
  }

  public void PlaceOrder(Order order)
  {
    Guard.Against.Null(order, nameof(order));
    _orders.Add(order);
  }

  public void ChangeEmail(string email)
  {
    Email = Guard.Against.NullOrWhiteSpace(email, nameof(email));
  }

  public void ChangeName(string name)
  {
    Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
  }

  public void ChangeRole(int roleId)
  {
    var newRole = Guard.Against.Null(UserRole.FromValue(roleId), nameof(roleId));

    if (Role == newRole)
      return;

    Role = UserRole.FromValue(roleId);
  }
}
