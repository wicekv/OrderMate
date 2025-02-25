using OrderMate.Core.Aggregates.UserAggregate.Enums;
using OrderMate.Core.Aggregates.UserAggregate.ErrorMessages;
using OrderMate.Core.Aggregates.UserAggregate.Specifications;
using OrderMate.Core.Aggregates.Users;

namespace OrderMate.UseCases.Users.Create;

public record CreateUserCommand(string Name, string Email) : ICommand<Result<int>>;

public sealed class CreateUserCommandHandler(IRepository<User> repository) : ICommandHandler<CreateUserCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var existingUser = await repository.FirstOrDefaultAsync(new UserByEmailSpecification(request.Email), cancellationToken);

    if(existingUser != null)
    {
      return Result.Invalid(new ValidationError(UserErrors.UserUniq));
    }

    var newUser = new User(request.Name, request.Email, UserRole.Customer);

    var result = await repository.AddAsync(newUser, cancellationToken);

    return result.Id;
  }
}
