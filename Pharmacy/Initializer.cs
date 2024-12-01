using Pharmacy.Domain.ModelsDb;
using Pharmacy.DAL.Interfaces;
using Pharmacy.DAL.Storage;
using Pharmacy.Service.Service;
using Pharmacy.Service.Interfaces;

namespace Pharmacy;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseStorage<UserDb>, UserStorage>();
    }

    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountServise, AccountService>();
        services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
    }
}