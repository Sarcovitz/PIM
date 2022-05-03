using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.RequestModels;

namespace PimApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCatalog createCatalog)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            int userId = Convert.ToInt32(User.FindFirst("Id").Value);

            int? catalogId = await _catalogService.CreateAsync(createCatalog, userId);

            return Ok(catalogId);
        }
    }
}
