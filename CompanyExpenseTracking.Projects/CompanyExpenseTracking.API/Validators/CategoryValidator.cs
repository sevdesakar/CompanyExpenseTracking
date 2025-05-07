using CompanyExpenseTracking.Domain.Entities;
using FluentValidation;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Kategori adı boş olamaz.")
            .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.");
    }
}