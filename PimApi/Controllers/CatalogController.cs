using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.DTO;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;
    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [Authorize]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateCatalog createCatalog)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int userId = Convert.ToInt32(User.FindFirst("Id").Value);

        int? catalogId = await _catalogService.CreateAsync(createCatalog, userId);

        return Ok(catalogId);
    }

    [Authorize]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAll()
    {
        int userId = Convert.ToInt32(User.FindFirst("Id").Value);

        List<CatalogDTO> catalogs = await _catalogService.GetAllAsync(userId);

        return Ok(catalogs);
    }

    [Authorize]
    [HttpGet]
    [Route("[action]/{catalogId:int}")]
    public async Task<IActionResult> Get([FromRoute] int catalogId)
    {
        int userId = Convert.ToInt32(User.FindFirst("Id").Value);

        CatalogDTO? catalog = await _catalogService.GetByIdAsync(catalogId, userId);

        if (catalog is null) return NotFound();

        return Ok(catalog);
    }

    [Authorize]
    [HttpPost]
    [Route("[action]/{catalogId:int}")]
    public async Task<IActionResult> Update([FromRoute] int catalogId, [FromBody] UpdateCatalog updateCatalog)
    {
        if(!ModelState.IsValid) return BadRequest();

        int resp = await _catalogService.UpdateAsync(catalogId, updateCatalog);

        if (resp <= 0) return BadRequest();
        return Ok();
        
    }

    [Authorize]
    [HttpGet]
    [Route("{catalogId:int}/Products")]
    public async Task<IActionResult> GetCatalogProducts([FromRoute] int catalogId)
    {
        return Ok();
    }

}
