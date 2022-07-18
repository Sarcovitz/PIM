using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PimApi.Services.Interfaces;
using PimModels.DTO;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributeController : ControllerBase
{
    private readonly IAttributeService _attributeService;
    public AttributeController(IAttributeService attributeService)
    {
        _attributeService = attributeService;
    }

    [Authorize]
    [HttpGet]
    [Route("Proto/All")]
    public async Task<IActionResult> GetAllProtos([FromQuery] int? catalogId, [FromQuery] int? categoryId)
    {
        List<ProductAttributeProto> attributeProtos = new();
        attributeProtos = await _attributeService.GetAllProtos(catalogId, categoryId);

        return Ok(attributeProtos);
    }

    [Authorize]
    [HttpGet]
    [Route("Proto/CategoryInherited")]
    public async Task<IActionResult> GetCategoryInherited([FromQuery] int categoryId)
    {
        List<CategoryProductAttributeProto> attributeProtos = new();
        attributeProtos = await _attributeService.GetCategoryInherited(categoryId);

        return Ok(attributeProtos);
    }

    [Authorize]
    [HttpGet]
    [Route("Proto")]
    public async Task<IActionResult> GetProto([FromQuery] int? id)
    {
        if (!id.HasValue) return NotFound("There is no id in query");

        ProductAttributeProtoDTO? atrProto = await _attributeService.GetProto(id.Value);

        if (atrProto is null) return NotFound($"There is no attribute prototype with id = {id}");
 
        return Ok(atrProto);
    }

    [Authorize]
    [HttpPost]
    [Route("Proto")]
    public async Task<IActionResult> CreateProto([FromBody] CreateProductAttributeProto createProto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        int protoId = await _attributeService.CreateProto(createProto);

        return Ok(protoId);
    }

    [Authorize]
    [HttpPut]
    [Route("Proto/{attributeId:int}")]
    public async Task<IActionResult> UpdateProto([FromBody] UpdateProductAttributeProto updateProto, [FromRoute]int attributeId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int resp = await _attributeService.UpdateProto(updateProto, attributeId);

        if (resp <= 0) return BadRequest();
        return Ok();
    }
}
