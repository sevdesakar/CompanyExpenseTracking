using CompanyExpenseTracking.Domain.Entities;
using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
            .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

        RuleFor(u => u.PasswordHash)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

        RuleFor(u => u.Role)
            .NotEmpty().WithMessage("Rol boş olamaz.")
            .Must(role => role == "Admin" || role == "Personel")
            .WithMessage("Rol sadece 'Admin' veya 'Personel' olabilir.");

        RuleFor(u => u.IBAN)
            .NotEmpty().When(u => u.Role == "Personel")
            .WithMessage("Personel için IBAN zorunludur.")
            .Matches(@"^TR\d{24}$").WithMessage("Geçerli bir IBAN giriniz.");
    }
}

