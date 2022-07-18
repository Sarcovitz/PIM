using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories(int? catalogId);
    Task<int> CreateAsync(CreateCategory createCategory);
    Task<Category?> GetById(int id);
}
