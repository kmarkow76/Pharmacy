using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Domain.ViewModels.LoginAndRegistration;

public class LoginViewMode
{
    [Required(ErrorMessage = "Введите почту")]
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной потчы")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set;}
}
