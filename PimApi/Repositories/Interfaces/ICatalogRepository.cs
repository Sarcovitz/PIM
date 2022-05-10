using PimModels.Models;

namespace PimApi.Repositories.Interfaces;

public interface ICatalogRepository
{
    Task<int?> CreateAsync(Catalog catalog, int userId);
    Task<List<Catalog>> GetUserCatalogsAsync(int userId);
    Task<Catalog?> GetByIdAsync(int catalogId);
    Task<int> UpdateAsync(Catalog catalog);
}
