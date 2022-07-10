using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.DTO;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services;

public class AttributeService : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository;
    public AttributeService(IAttributeRepository attributeRepository)
    {
        _attributeRepository = attributeRepository;
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
