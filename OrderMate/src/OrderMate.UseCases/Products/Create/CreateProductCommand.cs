using OrderMate.Core.Aggregates.ProductAggregate;
using OrderMate.Core.Aggregates.ProductAggregate.Enums;
using OrderMate.Core.Aggregates.ProductAggregate.ErrorMessages;

namespace OrderMate.UseCases.Products.Create;

public sealed record CreateProductCommand(string Name, decimal Price, int Stock, int CategoryId) : ICommand<Result<int>>;

public sealed class CreateProductCommandHandler(IRepository<Product> productRepository) : ICommandHandler<CreateProductCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
  {
    var category = ProductCategory.FromValue(request.CategoryId);

    if (category is null)
    {
      return Result.Invalid(new ValidationError(ProductErrors.CategoryNotFound));
    }

    var product = new Product(
      request.Name,
      request.Price,
      request.Stock,
      category);


    await productRepository.AddAsync(product, cancellationToken);
    await productRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(product.Id);
  }
}
