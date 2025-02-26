using FluentValidation;

namespace OrderMate.Web.v1.Users.List;
public sealed class GetUsersValidator : Validator<GetUsersRequest>
{
  public GetUsersValidator()
  {
    RuleFor(x => x.PageNumber)
      .GreaterThan(0);

    RuleFor(x => x.PageSize)
      .GreaterThan(0);
  }
}
