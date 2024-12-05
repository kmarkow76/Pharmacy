using FluentValidation;
using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Validators;

public class MedecineValidator: AbstractValidator<Medicine>
{
    public MedecineValidator()
    {
        RuleFor(medicine => medicine.Id).NotEmpty().WithMessage("Id медикамента обязателен.");
        RuleFor(medicine => medicine.Name).NotEmpty().MaximumLength(100).WithMessage("Имя не может превышать 100 символов. ");
        RuleFor(medicine => medicine.Description).MaximumLength(1000).WithMessage("Описание медикамента не может превышать 100 символов.");
        RuleFor(medicine => medicine.Price).GreaterThan(0).WithMessage("Цена должна быть больше 0.");
        RuleFor(medicine => medicine.Image).NotEmpty().WithMessage("Требуется URL-адрес изображения.");
        RuleFor(medicine => medicine.CategoryId).NotEmpty().WithMessage("Требуется Id категории.");
    }
    
}