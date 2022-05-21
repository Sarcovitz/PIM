using PimModels.Models;

namespace PimApi.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories(int? catalogId);
}
