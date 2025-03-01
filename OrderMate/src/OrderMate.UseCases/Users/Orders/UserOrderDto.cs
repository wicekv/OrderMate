namespace OrderMate.UseCases.Users.Orders;

public sealed record UserOrderDto(
    int OrderId,
    string Status,
    decimal TotalPrice,
    List<OrderItemDto> Items
);
