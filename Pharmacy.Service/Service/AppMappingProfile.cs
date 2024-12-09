using Pharmacy.Domain.ModelsDb;
using Pharmacy.Domain.Models;

using AutoMapper;
using Pharmacy.Domain.ViewModels;
using Pharmacy.Domain.ViewModels.Category;
using Pharmacy.Domain.ViewModels.LoginAndRegistration;
using Pharmacy.Domain.ViewModels.Medecine;

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
        CreateMap<Category, CategoryDb>().ReverseMap();
        CreateMap<Category, CategoryViewModel>().ReverseMap();
        CreateMap<Medicine, MedicineDb>().ReverseMap();
        CreateMap<Medicine, MedicinesForListOfMedicinesViewModel>().ReverseMap();
        CreateMap<Medicine, MedicinePageViewModel>().ReverseMap();
        
    }
    
}