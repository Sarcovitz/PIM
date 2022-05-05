using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;
    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCatalog createCatalog)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int userId = Convert.ToInt32(User.FindFirst("Id").Value);

        int? catalogId = await _catalogService.CreateAsync(createCatalog, userId);

        return Ok(catalogId);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult>GetAll()
    {
        int userId = Convert.ToInt32(User.FindFirst("Id").Value);

        List<Catalog> catalogs =  await _catalogService.GetAllAsync(userId);

        return Ok(catalogs);
    }
}
