using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.ViewModels.Category;
using Pharmacy.Domain.ViewModels.Medecine;
using Pharmacy.Service.Interfaces;
using Pharmacy.Service.Service;

namespace Pharmacy.Controllers;

public class MedicinesController : Controller
{
    private readonly IMedicineService _medicineService;
    private IMapper _mapper { get; set; }

    MapperConfiguration mapperConfiguration = new MapperConfiguration(p => { p.AddProfile<AppMappingProfile>(); });

    public MedicinesController(IMedicineService medicineService)
    {
        _medicineService = medicineService;
        _mapper = mapperConfiguration.CreateMapper();
    }

    public IActionResult ListOfMedicines(Guid Id)
    {
        var result = _medicineService.GetAllMedicinesByIdCategory(Id);
        ListOfMedicinesViewModel listMedicines = new ListOfMedicinesViewModel
        {
            Medicines = _mapper.Map<List<MedicinesForListOfMedicinesViewModel>>(result.Data),
            CategoryId = Id
        };
        return View(listMedicines);
    }
}