using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pharmacy.Domain.ViewModels.Category;
using Pharmacy.Service.Interfaces;
using Pharmacy.Service.Service;

namespace Pharmacy.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private IMapper _mapper { get; set; }

    MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        _mapper = mapperConfiguration.CreateMapper();
    }

    public IActionResult ListOfCategories()
    {
        var result = _categoryService.GetAllCategories();
        var listOfCategory = _mapper.Map<List<CategoryViewModel>>(result.Data);
        return View(listOfCategory);
    }
}