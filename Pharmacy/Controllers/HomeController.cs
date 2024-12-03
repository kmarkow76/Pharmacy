using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.ViewModels.LoginAndRegistration;
using Pharmacy.Domain.ModelsDb;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.ViewModels.LoginAndRegistration;
using Pharmacy.Service.Service;
using Pharmacy.Service.Interfaces;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private IMapper _mapper { get; set; }
        private readonly ILogger<HomeController> _logger;
        private IAccountServise _accountService { get; set; }
        private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        }); 
        public HomeController(ILogger<HomeController> logger, IAccountServise accountService, IWebHostEnvironment appEnvironment)
        {
            _accountService = accountService;
            _appEnvironment = appEnvironment;
            _mapper = mapperConfiguration.CreateMapper();
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
   public async Task<IActionResult> Login([FromBody] LoginViewMode model)
{
    if (ModelState.IsValid)
    {
        var user = _mapper.Map<User>(model);
        var response = await _accountService.Login(user);

        if (response.StatusCode == Domain.Enum.StatusCode.OK) // Убедитесь, что StatusCode определен
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));
            return Ok(model);
        }

        ModelState.AddModelError("", response.Description);
    }

    var errors = ModelState.Values.SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage)
        .ToList();
    return BadRequest(errors);
}

    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(model);
            var confirm = _mapper.Map<ConfirmEmailViewModel>(model);
            var code = await _accountService.Register(user);
            confirm.GeneratedCode = code.Data;
            return Ok(confirm);
        }
        var errors=ModelState.Values.SelectMany(v => v.Errors)
            .Select(e=>e.ErrorMessage)
            .ToList();
        return BadRequest(errors);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel model)
    {
        var user=_mapper.Map<User>(model);
        var response = await _accountService.ConfirmEmail(user, model.GeneratedCode, model.CodeConfirm);

        if (response.StatusCode==Domain.Enum.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));
            return Ok(model);
        }
        ModelState.AddModelError("",response.Description);
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(errors);
    }
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    public async Task AuthenticationGoogle(string returnUrl = "/") // По умолчанию возвращаемся на главную
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", new { returnUrl }), // Передаём returnUrl
                Parameters = { { "prompt", "select_account" } }
            });
    }

    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        try
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Succeeded == true)
            {
                var model = new User
                {
                    Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                    Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                    PartImage = "/" + (await SaveImageInImageUser(result.Principal.FindFirst("picture")?.Value, result)) ?? "/images/user.png"
                };

                var response = await _accountService.IsCreatedAccount(model);

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return Redirect(returnUrl);
                }

                return BadRequest("Аутентификация не удалась.");
            }

            return BadRequest("Результат аутентификации отсутствует или неудачен.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    private async Task<string> SaveImageInImageUser(string imageUrl, AuthenticateResult result)
    {
        string filePath = "";

        if (!string.IsNullOrEmpty(imageUrl))
        {
            using (var httpClient = new HttpClient())
            {
                // Формируем путь к файлу
                filePath = Path.Combine("ImageUser", $"{result.Principal.FindFirst(ClaimTypes.Email)?.Value}-avatar.jpg");

                // Получаем изображение по URL
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                // Сохраняем изображение в указанное место
                string fullPath = Path.Combine(_appEnvironment.WebRootPath, filePath);
                await System.IO.File.WriteAllBytesAsync(fullPath, imageBytes);
            }
        }

        return filePath;
    }
    
    }
}
