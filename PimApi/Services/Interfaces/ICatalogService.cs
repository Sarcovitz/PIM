using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface ICatalogService
{
    Task<int?> CreateAsync(CreateCatalog createCatalog, int UserId);
}
