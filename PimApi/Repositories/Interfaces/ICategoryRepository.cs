using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetById(int categoryId);
        Task<List<Category>> GetAll();
        Task<List<Category>> GetAllInCatalog(int catalogId);
        Task<int> CreateAsync(Category category, List<ProductAttributeProto>? attributeProtos);
        Task<int> UpdateAsync(int categoryId, UpdateCategory updateCategory);
    }
}
