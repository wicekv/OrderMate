using OrderMate.UseCases.Users.Orders.List;
using OrderMate.UseCases.Users.Orders;

namespace OrderMate.Web.v1.Users.Orders.List;

public sealed class GetUserOrdersEndpoint(IMediator _mediator)
    : Endpoint<GetUserOrdersRequest, List<UserOrderDto>>
{
  public override void Configure()
  {
    Get(GetUserOrdersRequest.Route);
    AllowAnonymous();

    Summary(s =>
    {
      s.Summary = "Get user orders with product details";
      s.Description = "Returns a list of orders for the specified user, including order items and product information.";
      s.ExampleRequest = new GetUserOrdersRequest { UserId = 1 };
    });
  }

  public override async Task HandleAsync(GetUserOrdersRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetUserOrdersQuery(request.UserId), cancellationToken);

    if (result.IsSuccess)
    {
      Response = result.Value;
      return;
    }

    if (result.Status == ResultStatus.Invalid)
    {
      foreach (var error in result.ValidationErrors)
      {
        AddError(error.ErrorMessage);
      }

      await SendErrorsAsync(cancellation: cancellationToken);
      return;
    }

    await SendErrorsAsync(cancellation: cancellationToken);
  }
}

