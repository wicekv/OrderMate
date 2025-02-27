using FluentValidation;
using OrderMate.Core.Aggregates.ProductAggregate.Enums;

namespace OrderMate.Web.v1.Products.Create;

public sealed class CreateProductValidator : Validator<CreateProductRequest>
{
  public CreateProductValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Nazwa produktu jest wymagana");

    RuleFor(x => x.Price)
        .GreaterThan(0)
        .WithMessage("Cena musi być większa niż 0");

    RuleFor(x => x.Stock)
        .GreaterThanOrEqualTo(0)
        .WithMessage("Stan magazynowy nie może być ujemny");

    RuleFor(x => x.CategoryId)
        .Must(BeValidCategory)
        .WithMessage("Niepoprawna kategoria produktu");
  }

  private bool BeValidCategory(int categoryId)
  {
    return ProductCategory.TryFromValue(categoryId, out _);
  }
}
