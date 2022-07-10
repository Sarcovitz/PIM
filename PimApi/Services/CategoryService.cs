using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.Models;

namespace PimApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Category>> GetAllCategories(int? catalogId)
    {
        if (catalogId.HasValue) return await _categoryRepository.GetAllInCatalog(catalogId.Value);
        else return await _categoryRepository.GetAll();
    }
}
