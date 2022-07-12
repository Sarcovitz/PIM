using PimModels.Models;

namespace PimApi.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<List<Category>> GetAllInCatalog(int catalogId);
        Task<int> CreateAsync(Category category);
    }
}
