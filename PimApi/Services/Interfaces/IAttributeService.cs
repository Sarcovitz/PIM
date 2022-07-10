using PimModels.DTO;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface IAttributeService
{
    Task<List<ProductAttributeProto>> GetAllProtos(int? catalogId, int? categoryId);
    Task<ProductAttributeProtoDTO?> GetProto(int id);
    Task<int> CreateProto(CreateProductAttributeProto createProto);
    Task<int> UpdateProto(UpdateProductAttributeProto updateProto, int attributeId);
}
