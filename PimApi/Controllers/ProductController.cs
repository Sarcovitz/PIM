using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;

namespace PimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll([FromRoute] int? catalogId)
        {
            if (!catalogId.HasValue) return BadRequest();
            return Ok(await _productService.GetAll(catalogId));
        }
    }
}
