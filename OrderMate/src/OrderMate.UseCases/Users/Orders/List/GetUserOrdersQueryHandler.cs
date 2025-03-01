using OrderMate.Core.Aggregates.OrderAggregate;
using OrderMate.Core.Aggregates.ProductAggregate;
using OrderMate.Core.Aggregates.ProductAggregate.Specifications;
using OrderMate.Core.Aggregates.UserAggregate.ErrorMessages;
using OrderMate.Core.Aggregates.UserAggregate.Specifications;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.UseCases.Users.Orders.List;

public sealed record GetUserOrdersQuery(int UserId) : IQuery<Result<List<UserOrderDto>>>;

public sealed class GetUserOrdersQueryHandler(
    IReadRepository<User> userRepository,
    IReadRepository<Product> productRepository)
    : IQueryHandler<GetUserOrdersQuery, Result<List<UserOrderDto>>>
{
  public async Task<Result<List<UserOrderDto>>> Handle(GetUserOrdersQuery query, CancellationToken cancellationToken)
  {
    var user = await userRepository.FirstOrDefaultAsync(new UserWithOrdersSpec(query.UserId), cancellationToken);

    if (user == null)
    {
      return Result.Invalid(new ValidationError(UserErrors.UserNotFound));
    }

    if (!user.Orders.Any())
    {
      return Result.Success(new List<UserOrderDto>());
    }

    var productIds = user.Orders
            .SelectMany(o => o.OrderItems)
            .Select(i => i.ProductId)
            .Distinct()
            .ToList();

    var products = await productRepository.ListAsync(new ProductsByIdsSpec(productIds), cancellationToken);
    var productDict = products.ToDictionary(p => p.Id);

    var ordersDto = MapToUserOrderDtos(user.Orders, productDict);

    return Result.Success(ordersDto);
  }

  private static List<UserOrderDto> MapToUserOrderDtos(IEnumerable<Order> orders, Dictionary<int, Product> productDict)
  {
    return orders
        .Select(o => new UserOrderDto(
            o.Id,
            o.Status.Name,
            o.GetTotalPrice(),
            o.OrderItems
                .Select(i => new OrderItemDto(
                    i.ProductId,
                    productDict.TryGetValue(i.ProductId, out var product) ? product.Name : "Unknown Product",
                    product?.Category.Name ?? "Unknown Category",
                    i.Quantity,
                    i.UnitPrice
                )).ToList()
        )).ToList();
  }
}
