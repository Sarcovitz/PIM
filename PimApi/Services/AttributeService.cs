using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.DTO;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services;

public class AttributeService : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository;
    private readonly ICategoryRepository _categoryRepository;
    public AttributeService(IAttributeRepository attributeRepository, ICategoryRepository categoryRepository)
    {
        _attributeRepository = attributeRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<int> CreateProto(CreateProductAttributeProto createProto)
    {
        var proto = new ProductAttributeProto(); 
        proto.Name = createProto.Name;
        proto.DefaultValue = createProto.DefaultValue;
        proto.PossibleValues = createProto.PossibleValues;
        proto.AttributeType = createProto.AttributeType;
        proto.CatalogId = createProto.CatalogId;

        int? protoId = await _attributeRepository.CreateProto(proto);
        return protoId ?? 0;
    }

    public async Task<ProductAttributeProtoDTO?> GetProto(int id)
    {
        ProductAttributeProto? proto = await _attributeRepository.GetProtoById(id);
        if (proto is null) return null;
        return ProtoToDTO(proto);
    }

    public async Task<List<ProductAttributeProto>> GetAllProtos(int? catalogId, int? categoryId)
    {
        if (categoryId.HasValue)  return await _attributeRepository.GetAllProtosByCategory(categoryId.Value);
        else if (catalogId.HasValue) return await _attributeRepository.GetAllProtosByCatalog(catalogId.Value);
        else return await _attributeRepository.GetAllProtos();
    }

    public async Task<List<CategoryProductAttributeProto>> GetCategoryInherited(int categoryId)
    {
        List<CategoryProductAttributeProto> list = new();
        var category = await _categoryRepository.GetById(categoryId);
        if (category is null) return list;
        if (category.AttributeProtos is not null) list.AddRange(category.AttributeProtos);
        
        while(category.ParentCategoryId.HasValue)
        {
            category = await _categoryRepository.GetById(category.ParentCategoryId.Value);
            if (category.AttributeProtos is not null) list.AddRange(category.AttributeProtos);
        }

        return list;
    }

    public async Task<int> UpdateProto(UpdateProductAttributeProto updateProto, int attributeId)
    {
        ProductAttributeProto proto = new ProductAttributeProto();
        proto.Id = attributeId;
        proto.Name = updateProto.Name;
        proto.DefaultValue = updateProto.DefaultValue;
        proto.PossibleValues = updateProto.PossibleValues;
        proto.AttributeType = updateProto.AttributeType;

        return await _attributeRepository.UpdateAsync(proto);
    }

    // Out of interface

    private ProductAttributeProtoDTO ProtoToDTO(ProductAttributeProto proto)
    {
        var dto = new ProductAttributeProtoDTO();
        dto.Id = proto.Id;
        dto.Name = proto.Name;
        dto.DefaultValue = proto.DefaultValue;
        dto.PossibleValues = proto.PossibleValues;
        dto.Categories = proto.Categories;
        dto.AttributeType = proto.AttributeType;

        return dto;
    }
}
