using OrderMate.UseCases.Users.Create;

namespace OrderMate.Web.v1.Users.Create;

public sealed class CreateUserEndpoint(IMediator _mediator) 
  : Endpoint<CreateUserRequest, CreateUserResponse>
{
  public override void Configure()
  {
    Post("/api/users");
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Creating a new user";
      s.Description = "Endpoint for creating a new user with the specified data.";
      s.ExampleRequest = new CreateUserRequest("John Doe", "john.doe@example.com");
    });
  }

  public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateUserCommand(request.Name, request.Email), cancellationToken);

    if(result.Status == ResultStatus.Invalid)
    {
      foreach(var error in result.ValidationErrors)
      {
        AddError(error.ErrorMessage);
      }

      await SendErrorsAsync(cancellation: cancellationToken);
    }

    if(result.IsSuccess)
    {
      Response = new CreateUserResponse(result.Value, request.Name!);
    }

  }
}

