namespace OrderMate.UseCases.Users.Orders;

public record OrderItemDto(
    int ProductId,
    string ProductName,
    string Category,
    int Quantity,
    decimal UnitPrice
);
