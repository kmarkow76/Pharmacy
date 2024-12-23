using Pharmacy.Domain.Models;
using Pharmacy.Domain.Response;
using Pharmacy.Domain.ViewModels;

namespace Pharmacy.Service.Interfaces;

public interface IMedicineService
{
    BaseResponse<List<Medicine>> GetAllMedicinesByIdCategory(Guid Id);
    BaseResponse<List<Medicine>> GetMedicinesByFilter(MedicineFilter filter);
    Task<BaseResponse<Medicine>> GetMedicinesById(Guid Id);
    

}