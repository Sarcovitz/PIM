using PimModels.Models;

namespace PimApi.Repositories.Interfaces;

public interface ICatalogRepository
{
    Task<int?> CreateAsync(Catalog catalog, int userId);
}
