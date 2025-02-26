using FluentValidation;

namespace OrderMate.Web.v1.Orders.Create;

public class CreateOrderValidator : Validator<CreateOrderRequest>
{
  public CreateOrderValidator()
  {
    RuleFor(x => x.UserId)
      .GreaterThan(0)
      .WithMessage("UserId musi być większe niż 0");

    RuleFor(x => x.Items)
      .NotEmpty()
      .WithMessage("Lista produktów nie może być pusta");

    RuleForEach(x => x.Items).ChildRules(item =>
    {
      item.RuleFor(x => x.ProductId)
      .GreaterThan(0)
      .WithMessage("ProductId musi być większe niż 0");

      item.RuleFor(x => x.Quantity)
      .GreaterThan(0)
      .WithMessage("Quantity musi być większe niż 0");
    });
  }
}
