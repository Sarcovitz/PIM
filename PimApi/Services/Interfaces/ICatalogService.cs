using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface ICatalogService
{
    Task<int?> CreateAsync(CreateCatalog createCatalog, int UserId);
    Task<List<Catalog>> GetAllAsync(int UserId);
}
