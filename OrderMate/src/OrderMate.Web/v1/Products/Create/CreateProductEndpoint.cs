using OrderMate.UseCases.Products.Create;

namespace OrderMate.Web.v1.Products.Create;

public class CreateProductEndpoint(IMediator mediator)
    : Endpoint<CreateProductRequest, CreateProductResponse>
{
  public override void Configure()
  {
    Post(CreateProductRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Create a new product";
      s.Description = "Creates a new product with a category, price, and stock level.";
      s.ExampleRequest = new CreateProductRequest
      {
        Name = "Laptop",
        Price = 4999.99m,
        Stock = 100,
        CategoryId = 1
      };
    });
  }

  public override async Task HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
  {
    var command = new CreateProductCommand(
        request.Name!,
        request.Price,
        request.Stock,
        request.CategoryId
    );

    var result = await mediator.Send(command, cancellationToken);

    if (result.Status == ResultStatus.Invalid)
    {
      foreach (var error in result.ValidationErrors)
      {
        AddError(error.ErrorMessage);
      }

      await SendErrorsAsync(cancellation: cancellationToken);
      return;
    }

    Response = new CreateProductResponse(result.Value);
  }
}
