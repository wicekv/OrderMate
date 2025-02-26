using OrderMate.UseCases.Orders;

namespace OrderMate.Web.v1.Orders.Create;

public sealed class CreateOrderRequest
{
  public const string Route = "/api/orders";

  public int UserId { get; init; }
  public IEnumerable<OrderItemDto> Items { get; init; } = [];
}
