using Pharmacy.Domain.ModelsDb;
using Pharmacy.Domain.Models;

using AutoMapper;
using Pharmacy.Domain.ViewModels.LoginAndRegistration;

namespace Pharmacy.Service.Service;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDb>().ReverseMap();
        CreateMap<User, LoginViewMode>().ReverseMap();
        CreateMap<User, RegisterViewModel>().ReverseMap();
        CreateMap<RegisterViewModel,ConfirmEmailViewModel>().ReverseMap();
        CreateMap<User,ConfirmEmailViewModel>().ReverseMap();
    }
    
}