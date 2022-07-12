using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<int> CreateAsync(CreateCategory createCategory)
    {
        if ((await GetAllCategories(createCategory.CatalogId)).Any(c => c.Name.ToLower() == createCategory.Name.ToLower())) throw new Exception("This catalog already have category with the same name.");

        Category category = new();
        category.Name = createCategory.Name;
        category.AttributeProtos = createCategory.AttributeProtos;
        category.CatalogId = createCategory.CatalogId;
        category.ParentCategoryId = createCategory.ParentCategoryId;

        int categoryId = await _categoryRepository.CreateAsync(category);

        return categoryId;
    }

    public async Task<List<Category>> GetAllCategories(int? catalogId)
    {
        if (catalogId.HasValue) return await _categoryRepository.GetAllInCatalog(catalogId.Value);
        else return await _categoryRepository.GetAll();
    }
}
