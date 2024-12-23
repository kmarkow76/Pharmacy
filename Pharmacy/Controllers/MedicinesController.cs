using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Response;
using Pharmacy.Domain.ViewModels;
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

    [HttpPost]
    public async Task<IActionResult> Filter([FromBody] MedicineFilter filter)
    {
        var result = _medicineService.GetMedicinesByFilter(filter);
        var filteredMedicines = _mapper.Map<List<MedicinesForListOfMedicinesViewModel>>(result.Data);
        return Json(filteredMedicines);
    }


    public async Task<IActionResult> MedicinePage(Guid Id)
    {
        var resultMedicine = await _medicineService.GetMedicinesById(Id);
        MedicinePageViewModel medicine = _mapper.Map<MedicinePageViewModel>(resultMedicine.Data);
        return View(medicine);
    }


}



