using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.DTO;
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
        if ((await GetAllAsync(userId)).Any(c => c.Name.ToLower() == createCatalog.Name.ToLower())) throw new Exception("This user already have catalog with the same name.");

        Catalog catalog = new(); 
        catalog.Name = createCatalog.Name;
        catalog.DefaultCurrencyCode = createCatalog.DefaultCurrencyCode;

        int? catalogId = await _catalogRepository.CreateAsync(catalog, userId);

        return catalogId;
    }

    public async Task<List<CatalogDTO>> GetAllAsync(int userId)
    {
        List<CatalogDTO> list = new List<CatalogDTO>();
        var catalogs = await _catalogRepository.GetUserCatalogsAsync(userId);
        foreach (var catalog in catalogs)
        {
            if (catalog is null) continue;
            list.Add(CatalogToDto(catalog));
        }

        return list;
    }

    public async Task<CatalogDTO?> GetByIdAsync(int catalogId, int userId)
    {
        var catalog = await _catalogRepository.GetByIdAsync(catalogId);
        if (!catalog.CatalogUsers.Any(x => x.UserId == userId)) return null;
        if (catalog is null) return null;
        return CatalogToDto(catalog);
    }

    public async Task<int> UpdateAsync(int catalogId, UpdateCatalog updateCatalog)
    {
        Catalog catalog = new Catalog();
        catalog.Id = catalogId;
        catalog.Name = updateCatalog.Name;
        catalog.DefaultCurrencyCode = updateCatalog.DefaultCurrencyCode;
        catalog.CatalogUsers = updateCatalog.CatalogUsers;

        return await _catalogRepository.UpdateAsync(catalog);
    }

    //Out of interface

    private CatalogDTO CatalogToDto(Catalog catalog)
    {
        CatalogDTO dto = new CatalogDTO();
        dto.Id = catalog.Id;
        dto.Name = catalog.Name;
        dto.DefaultCurrencyCode = catalog.DefaultCurrencyCode;
        dto.CatalogUsers = catalog.CatalogUsers;

        return dto;
    }
}
