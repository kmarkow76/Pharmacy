using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.ViewModels.LoginAndRegistration;
using System.Linq;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewMode model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(errors); // Возврат ошибок 400 с деталями
            }

            // Логика авторизации (например, проверка пользователя)
            return Ok(model); // Возвращаем успешный ответ
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(errors); // Возврат ошибок 400 с деталями
            }

            // Логика регистрации пользователя
            return Ok(model); // Возвращаем успешный ответ
        }

    }
}
