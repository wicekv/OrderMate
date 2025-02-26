namespace OrderMate.Web.v1.Users.Create;

public sealed class CreateUserRequest
{
  public const string Route = "/api/users";

  public required string Name { get; init; }

  public required string Email { get; init; }
}

