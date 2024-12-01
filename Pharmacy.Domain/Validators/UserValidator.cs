using FluentValidation;
using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Validators;

public class UserValidator:AbstractValidator<User>
{
    public UserValidator()
    {
        // Валидация пароля
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 6 символов");

        // Валидация email
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Неверный формат Email");
    }
    
}