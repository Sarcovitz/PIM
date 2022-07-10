using PimModels.Models;

namespace PimApi.Repositories.Interfaces;

public interface IAttributeRepository
{
    Task<int?> CreateProto(ProductAttributeProto proto);
    Task<List<ProductAttributeProto>> GetAllProtos();
    Task<List<ProductAttributeProto>> GetAllProtosByCategory(int categoryId);
    Task<List<ProductAttributeProto>> GetAllProtosByCatalog(int catalogId);
    Task<ProductAttributeProto?> GetProtoById(int id);
    Task<int> UpdateAsync(ProductAttributeProto proto);

}
