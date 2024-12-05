using Pharmacy.Domain.ModelsDb;
using Pharmacy.DAL.Interfaces;
using Pharmacy.DAL.Storage;
using Pharmacy.Domain.Models;
using Pharmacy.Service.Service;
using Pharmacy.Service.Interfaces;

namespace Pharmacy;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
        services.AddScoped<IBaseStorage<CategoryDb>, CategoryStorage>();
        services.AddScoped<IBaseStorage<MedicineDb>, MedicineStorage>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountServise, AccountService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IMedicineService, MedicineService>();
        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
    }
}