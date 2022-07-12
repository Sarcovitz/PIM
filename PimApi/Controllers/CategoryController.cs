﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.Models;
using PimModels.RequestModels;

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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateCategory createCategory)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int userId = Convert.ToInt32(User.FindFirst("Id").Value);

        int categoryId = await _categoryService.CreateAsync(createCategory);

        return Ok(categoryId);
    }


    [HttpGet]
    [Authorize]
    [Route("All")]
    public async Task<IActionResult> GetAll([FromQuery] int? catalogId)
    {
        var categories = new List<Category>();
        categories = await _categoryService.GetAllCategories(catalogId);
        return Ok(categories);
    }
}
