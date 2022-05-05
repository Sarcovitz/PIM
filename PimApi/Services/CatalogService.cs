using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task<int?> CreateAsync(CreateCatalog createCatalog, int userId)
    {
        if ((await GetAllAsync(userId)).Any(c => c.Name.ToLower() == createCatalog.Name.ToLower())) 
            throw new Exception("This user already have catalog with the same name.");

        Catalog catalog = new Catalog(); 
        catalog.Name = createCatalog.Name;
        catalog.DefaultCurrencyCode = createCatalog.DefaultCurrencyCode;

        int? catalogId = await _catalogRepository.CreateAsync(catalog, userId);

        return catalogId;
    }

    public async Task<List<Catalog>> GetAllAsync(int userId) => await _catalogRepository.GetAllAsync(userId);
}
