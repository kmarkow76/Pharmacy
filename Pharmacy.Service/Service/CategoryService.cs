using AutoMapper;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.Enum;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.ModelsDb;
using Pharmacy.Domain.Response;
using Pharmacy.Domain.Validators;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.Service.Service;

public class CategoryService : ICategoryService
{
    private readonly IBaseStorage<CategoryDb> _categoryStorage;
    private IMapper _mapper { get; set; }
    private CategoryValidator _validationRules { get; set; }

    MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public CategoryService(IBaseStorage<CategoryDb> categoryStorage)
    {
        _categoryStorage = categoryStorage;
        _mapper = mapperConfiguration.CreateMapper();
        _validationRules = new CategoryValidator();
    }
    public BaseResponse<List<Category>> GetAllCategories()
    {
        try
        {
            var categoryDb = _categoryStorage.GetAll().OrderBy(p => p.CreatedAt).ToList();
            var result = _mapper.Map<List<Category>>(categoryDb);
        
            if (result.Count == 0)
            {
                return new BaseResponse<List<Category>>()
                {
                    Description = "Ни одного элемента не найдено",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Category>>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Category>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

}