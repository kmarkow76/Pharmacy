using Pharmacy.Domain.Models;
using Pharmacy.Domain.Response;

namespace Pharmacy.Service.Interfaces;

public interface ICategoryService
{
    BaseResponse<List<Category>> GetAllCategories();
}