using OrderMate.UseCases.Orders;
using OrderMate.UseCases.Orders.Create;

namespace OrderMate.Web.v1.Orders.Create;

public sealed class CreateOrderEndpoint(IMediator mediator)
  : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
  public override void Configure()
  {
    Post(CreateOrderRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Create a new order";
      s.Description = "Endpoint for creating an order with the specified products.";
      s.ExampleRequest = new CreateOrderRequest
      {
        UserId = 1,
        Items = new List<OrderItemDto>
        {
          new OrderItemDto(10, 2),
          new OrderItemDto(1, 15),
        }
      };
    });
  }

  public override async Task HandleAsync(CreateOrderRequest request, CancellationToken cancellationToken)
  {
    var result = await mediator.Send(new CreateOrderCommand(request.UserId, request.Items), cancellationToken);

    if (result.Status == ResultStatus.Invalid)
    {
      foreach (var error in result.ValidationErrors)
      {
        AddError(error.ErrorMessage);
      }

      await SendErrorsAsync(cancellation: cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new CreateOrderResponse(result.Value);
      return;
    }
  }

}
