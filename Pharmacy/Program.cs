using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Pharmacy;
using Pharmacy.DAL;
using Pharmacy.Domain.ModelsDb;
using Pharmacy.Service.Interfaces;
using Pharmacy.Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов для работы с контроллерами и представлениями
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
// Получение строки подключения и настройка контекста базы данных
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));

// Настройка аутентификации
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
        options.Scope.Add("profile");
        options.ClaimActions.MapJsonKey("picture", "picture");
    });

// Инициализация репозиториев и сервисов
builder.Services.InitializeRepositories();
builder.Services.InitializeServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Конфигурация HTTP запроса
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


// Настройка аутентификации и авторизации
app.UseAuthentication();  // Аутентификация должна быть перед авторизацией
app.UseAuthorization();   // Авторизация после аутентификации

// Маршрутизация
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
});

app.Run();
