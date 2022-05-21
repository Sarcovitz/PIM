using PimApi.Services.Interfaces;
using PimModels.Models;

namespace PimApi.Services;

public class CategoryService : ICategoryService
{
    public Task<List<Category>> GetAllCategories(int? catalogId)
    {
        throw new NotImplementedException();
    }
}
