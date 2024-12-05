using AutoMapper;
using Pharmacy.DAL.Interfaces;
using Pharmacy.Domain.Enum;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.ModelsDb;
using Pharmacy.Domain.Response;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.Service.Service;

public class MedicineService : IMedicineService
{
    private readonly IBaseStorage<MedicineDb> _medStorage;
    private IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
      p.AddProfile<AppMappingProfile>();
    });

    public MedicineService(IBaseStorage<MedicineDb> medStorage, IMapper mapper)
    {
        _medStorage = medStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }
    public BaseResponse<List<Medicine>> GetAllMedicinesByIdCategory(Guid Id)
    {
        try
        {
            var medicineDb = _medStorage.GetAll().Where(x=>Id==x.CategoryId).OrderBy(p => p.CreatedAt).ToList();
            var result = _mapper.Map<List<Medicine>>(medicineDb);
        
            if (result.Count == 0)
            {
                return new BaseResponse<List<Medicine>>()
                {
                    Description = "Ни одного элемента не найдено",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<Medicine>>()
            {
                Data = result,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Medicine>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}