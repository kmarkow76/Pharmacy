using FluentValidation;
using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.Id).NotEmpty().WithMessage("Id заказа обязателен.");;
        RuleFor(order => order.UserId).NotEmpty().WithMessage("Id пользователя обязателен.");;
        RuleFor(order => order.TotalPrice).GreaterThan(0).WithMessage("Общая цена не должна быть больше 0.");
        RuleFor(order => order.Status).IsInEnum().WithMessage("Неправильный статус из перечисления");
        RuleFor(order => order.CreatedAt).GreaterThan(TimeSpan.Zero).WithMessage("Время добавления не может быть будущего времени.");
    }
    
}