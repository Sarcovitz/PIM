using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.RequestModels;

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
        public async Task<IActionResult> GetAll(int? catalogId)
        {
            if (!catalogId.HasValue) return BadRequest();
            return Ok(await _productService.GetAll(catalogId));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProduct createProduct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _productService.CreateAsync(createProduct));
        }

        [Authorize]
        [HttpGet]
        [Route("{productId:int}")]
        public async Task<IActionResult> Get([FromRoute] int productId) => Ok(await _productService.GetProductAsync(productId));

        [Authorize]
        [HttpPut]
        [Route("{productId:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateProduct updateProduct, [FromRoute] int productId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int resp = await _productService.UpdateAsync(productId, updateProduct);

            if (resp <= 0) return BadRequest();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("Photo")]
        public async Task<IActionResult> GetMainPhoto(int productId)
        {
            if (productId == 0) return BadRequest("Product Id cannot be 0");
            return Ok(await _productService.GetMainPhoto(productId));
        }
    }
}
