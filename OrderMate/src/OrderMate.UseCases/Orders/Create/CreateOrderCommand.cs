using OrderMate.Core.Aggregates.OrderAggregate;
using OrderMate.Core.Aggregates.ProductAggregate;
using OrderMate.Core.Aggregates.ProductAggregate.ErrorMessages;
using OrderMate.Core.Aggregates.UserAggregate.ErrorMessages;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.UseCases.Orders.Create;

public sealed record CreateOrderCommand(int UserId, IEnumerable<OrderItemDto> Items) : ICommand<Result<int>>;

public sealed class CreateOrderCommandHandler(
  IRepository<Product> productRepository,
  IRepository<User> userRepository) : ICommandHandler<CreateOrderCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

    if (user is null)
    {
      return Result.Invalid(new ValidationError(UserErrors.UserNotFound));
    }

    var order = new Order(request.UserId);

    foreach (var item in request.Items)
    {
      var product = await productRepository.GetByIdAsync(item.ProductId, cancellationToken);

      if (product is null)
      {
        return Result.Invalid(new ValidationError(ProductErrors.ProductNotFound));
      }

      if (product.Stock < item.Quantity)
      {
        return Result.Invalid(new ValidationError(ProductErrors.ProductsNotInStock));
      }

      product.UpdateStock(item.Quantity);
      order.AddProduct(product, item.Quantity);
    }

    user.PlaceOrder(order);

    await userRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(order.Id);
  }
}
