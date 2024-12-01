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
using Pharmacy.Domain.Enum;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        private IMapper _mapper { get; set; }
        private readonly ILogger<HomeController> _logger;
        private IAccountServise _accountService { get; set; }
        private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        }); 
        public HomeController(ILogger<HomeController> logger, IAccountServise accountService)
        {
            _accountService = accountService;
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
    }
}
