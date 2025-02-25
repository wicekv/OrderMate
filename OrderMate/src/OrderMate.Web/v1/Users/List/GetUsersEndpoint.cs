using OrderMate.UseCases.Users.List;

namespace OrderMate.Web.v1.Users.List;

public sealed class GetUsersEndpoint(IMediator _mediator) : Endpoint<GetUsersRequest, PagedResult<List<GetUsersResponse>>>
{
  public override void Configure()
  {
    Get("/api/users");
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Get user lists with pagination";
      s.Description = "returns the pagination list with users";
      s.ExampleRequest = new GetUsersRequest(1, 10);
    });
  }

  public override async Task HandleAsync(GetUsersRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetUsersQuery(request.PageNumber, request.PageSize));

    if(result.IsSuccess)
    {
      var mappedUsers = result.Value.Select(u => new GetUsersResponse(u.Name, u.Email)).ToList();

      Response = new PagedResult<List<GetUsersResponse>>(result.PagedInfo, mappedUsers);

      return;
    }
  }
}
