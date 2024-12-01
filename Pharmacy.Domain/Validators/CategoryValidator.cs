using FluentValidation;
using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Id).NotEmpty().WithMessage("Id категории обязателен.");
        RuleFor(category => category.Name).NotEmpty().MaximumLength(100).WithMessage("Имя не может превышать 100 символов.");
        RuleFor(category => category.Description).MaximumLength(500).WithMessage("Описание категории не может превышать 500 символов.");
        RuleFor(category => category.CreatedAt).LessThanOrEqualTo(DateTime.Now).WithMessage("Время добавления не может быть будущего времени.");
    }
}