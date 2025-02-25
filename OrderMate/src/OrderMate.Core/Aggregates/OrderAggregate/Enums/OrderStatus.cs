namespace OrderMate.Core.Aggregates.OrderAggregate.Enums;

public class OrderStatus : SmartEnum<OrderStatus>
{
  public static readonly OrderStatus New = new(nameof(New), 1);
  public static readonly OrderStatus InProgress = new(nameof(InProgress), 2);
  public static readonly OrderStatus Completed = new(nameof(Completed), 3);

  private OrderStatus(string name, int value) : base(name, value) { }
}
