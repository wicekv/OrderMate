using OrderMate.Core.Aggregates.OrderAggregate.Enums;
using OrderMate.Core.Aggregates.ProductAggregate;

namespace OrderMate.Core.Aggregates.OrderAggregate;
public class Order : EntityBase, IAggregateRoot
{
  public int UserId { get; private set; }
  public OrderStatus Status { get; private set; } = default!;

  private readonly List<OrderItem> _orderItems = new();
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

  private Order() { }

  public Order(int userId)
  {
    UserId = Guard.Against.NegativeOrZero(userId, nameof(userId));
    Status = OrderStatus.New;
  }

  public void AddProduct(Product product, int quantity)
  {
    Guard.Against.Null(product, nameof(product));
    Guard.Against.NegativeOrZero(quantity, nameof(quantity));

    var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == product.Id);

    if (existingItem != null)
    {
      existingItem.IncreaseQuantity(quantity);
    }
    else
    {
      _orderItems.Add(new OrderItem(product.Id, quantity, product.Price));
    }
  }

  public decimal GetTotalPrice()
  {
    return _orderItems.Sum(item => item.TotalPrice);
  }

  public void ChangeStatus(OrderStatus newStatus)
  {
    Guard.Against.Null(newStatus, nameof(newStatus));
    Status = newStatus;
  }
}
