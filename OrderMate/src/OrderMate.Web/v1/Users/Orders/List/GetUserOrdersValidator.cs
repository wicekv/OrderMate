using FluentValidation;

namespace OrderMate.Web.v1.Users.Orders.List;

public class GetUserOrdersValidator : Validator<GetUserOrdersRequest>
{
  public GetUserOrdersValidator()
  {
    RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId musi być większe niż 0");
  }
}
