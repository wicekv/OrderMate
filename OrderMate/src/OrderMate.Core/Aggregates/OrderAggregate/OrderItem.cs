namespace OrderMate.Core.Aggregates.OrderAggregate;

public class OrderItem : EntityBase
{
  public int OrderId { get; private set; }
  public int ProductId { get; private set; }
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }

  public decimal TotalPrice => Quantity * UnitPrice;

  private OrderItem() { }

  public OrderItem(int productId, int quantity, decimal unitPrice)
  {
    ProductId = Guard.Against.NegativeOrZero(productId, nameof(productId));
    Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
    UnitPrice = Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice));
  }

  public void IncreaseQuantity(int amount)
  {
    Quantity += Guard.Against.NegativeOrZero(amount, nameof(amount));
  }
}
