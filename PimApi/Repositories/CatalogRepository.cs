using PimApi.Data;
using PimApi.Repositories.Interfaces;
using PimModels.Models;

namespace PimApi.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly PimDbContext _context; 
    public CatalogRepository(PimDbContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(Catalog catalog, int userId)
    {
        using (var tran = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.Catalogs.AddAsync(catalog);
                await _context.SaveChangesAsync();

                await _context.CatalogUsers.AddAsync(new CatalogUser { CatalogId = catalog.Id, UserId = userId, Role = CatalogUserRole.Owner });
                await _context.SaveChangesAsync();

                await tran.CommitAsync();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message);
            }
        }

        return catalog.Id;
    }
}
