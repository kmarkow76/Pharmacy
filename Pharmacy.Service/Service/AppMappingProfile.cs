using Pharmacy.Domain.ModelsDb;
using Pharmacy.Domain.Models;

using AutoMapper;
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
        CreateMap<CategoryDb, CategoryViewModel>()
            .ForMember(dest => dest.PathImg, opt => opt.MapFrom(src => src.PathImage)) // Если названия отличаются
            .ForMember(dest => dest.CountMed, opt => opt.MapFrom(src => src.Medicines.Count));
        CreateMap<Medicine, MedicineDb>().ReverseMap();
        CreateMap<Medicine, MedicinesForListOfMedicinesViewModel>().ReverseMap();
    }
    
}