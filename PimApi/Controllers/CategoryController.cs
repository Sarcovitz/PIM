using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;

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

    public async Task<IActionResult> GetAll([FromRoute] int? catalogId)
    {

        return Ok();
    }
}
