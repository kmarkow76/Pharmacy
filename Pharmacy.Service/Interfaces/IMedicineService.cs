using Pharmacy.Domain.Models;
using Pharmacy.Domain.Response;

namespace Pharmacy.Service.Interfaces;

public interface IMedicineService
{
    BaseResponse<List<Medicine>> GetAllMedicinesByIdCategory(Guid Id);
}