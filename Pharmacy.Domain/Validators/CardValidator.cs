using FluentValidation;
using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Validators;

public class CardValidator : AbstractValidator<Card>
{
    public CardValidator()
    {
        RuleFor(card => card.Id).NotEmpty().WithMessage("Id корзины обязателен.");
        RuleFor(card => card.UserId).NotEmpty().WithMessage("Id пользователя обязателен");
        RuleFor(card => card.MedicineId).NotEmpty().WithMessage("Id медикамента обязателен");
        RuleFor(card => card.Quantity).GreaterThan(0).WithMessage("Количество должно быть больше 0");
        RuleFor(card => card.Price).GreaterThan(0).WithMessage("Цена должна быть больше 0");
        RuleFor(card => card.AddedAt).LessThanOrEqualTo(DateTime.Now).WithMessage("Время добавления не может быть будущего времени");
    }
}