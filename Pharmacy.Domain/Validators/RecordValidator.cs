using FluentValidation;
using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Validators;

public class RecordValidator : AbstractValidator<Record>
{
    public RecordValidator()
    {
        RuleFor(record => record.Id).NotEmpty().WithMessage("Id записи обязателен.");;
        RuleFor(record => record.Email)
            .NotEmpty().WithMessage("Email не может быть пустым.")
            .EmailAddress().WithMessage("Неправильный формат почты.")
            .Matches(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")
            .WithMessage("Email не соответствует допустимому формату.");
        RuleFor(record => record.Description).NotEmpty().MaximumLength(1000).WithMessage("Описание не может превышать 1000 символов.");
    }
}