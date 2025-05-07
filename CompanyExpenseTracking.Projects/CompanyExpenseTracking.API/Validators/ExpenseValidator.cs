using CompanyExpenseTracking.Domain.Entities;
using FluentValidation;

public class ExpenseValidator : AbstractValidator<Expense>
{
    public ExpenseValidator()
    {
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

        RuleFor(e => e.Amount)
            .GreaterThan(0).WithMessage("Tutar sıfırdan büyük olmalıdır.");

        RuleFor(e => e.Date)
            .NotEmpty().WithMessage("Tarih boş olamaz.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Tarih bugünden ileri olamaz.");

        RuleFor(e => e.CategoryId)
            .GreaterThan(0).WithMessage("Geçerli bir kategori seçilmelidir.");

        RuleFor(e => e.Status)
            .Must(status => status == "Pending" || status == "Approved" || status == "Rejected")
            .WithMessage("Geçersiz durum. Durum 'Pending', 'Approved' veya 'Rejected' olmalıdır."); 
    }
}
