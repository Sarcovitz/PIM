using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Catalog>> GetUserCatalogsAsync(int userId)
        => await _context.Catalogs.Where(c => c.CatalogUsers.Any(cu => cu.UserId == userId)).Include(c => c.CatalogUsers).ThenInclude(cu => cu.User).ToListAsync();//.ThenInclude(cu => cu.User).ToListAsync();

    public async Task<Catalog?> GetByIdAsync(int catalogId)
        => await _context.Catalogs.Where(c => c.Id == catalogId).Include(c => c.CatalogUsers).ThenInclude(c => c.User).FirstOrDefaultAsync();

    public async Task<int> UpdateAsync(Catalog model)
    {
        //_context.Catalogs.Update(model);
        var _catalog = await GetByIdAsync(model.Id);
        if (_catalog is null) return 0;
        _catalog.Name = model.Name;
        _catalog.DefaultCurrencyCode = model.DefaultCurrencyCode;
        _catalog.CatalogUsers = model.CatalogUsers;
        _context.Catalogs.Update(_catalog);

        //var catalogUsersToDelete = _catalog.CatalogUsers.Where(cuDb => !model.CatalogUsers.Any(cuModel => cuModel.UserId == cuDb.UserId)).ToList();

        //foreach(var cu in catalogUsersToDelete)
        //{
        //    _catalog.CatalogUsers.Remove(cu);
        //}

        return await _context.SaveChangesAsync();
    }
}
