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
        Catalog catalog = new Catalog(); 
        catalog.Name = createCatalog.Name;
        catalog.DefaultCurrency = createCatalog.DefaultCurrency;
        //catalog.DefualtCurrencyCode = createCatalog.DefaultCurrency.Code;

        int? catalogId = await _catalogRepository.CreateAsync(catalog, userId);

        return catalogId;
    }
}
