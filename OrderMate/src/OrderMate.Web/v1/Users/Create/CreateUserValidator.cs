using FluentValidation;

namespace OrderMate.Web.v1.Users.Create;

public sealed class CreateUserValidator : Validator<CreateUserRequest>
{
  public CreateUserValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(30);

    RuleFor(x => x.Email)
      .EmailAddress()
      .NotEmpty();
  }
}
