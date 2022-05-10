using PimModels.DTO;
using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface ICatalogService
{
    Task<int?> CreateAsync(CreateCatalog createCatalog, int UserId);
    Task<List<CatalogDTO>> GetAllAsync(int UserId);
    Task<CatalogDTO?> GetByIdAsync(int catalogId, int userId);
    Task<int> UpdateAsync(int catalogId, UpdateCatalog updateCatalog);
}
