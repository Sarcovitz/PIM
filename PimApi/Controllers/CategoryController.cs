using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.Models;

namespace PimApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] int? catalogId)
    {
        var categories = new List<Category>();
        categories = await _categoryService.GetAllCategories(catalogId);
        return Ok(categories);
    }
}
