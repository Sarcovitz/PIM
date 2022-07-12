using Microsoft.EntityFrameworkCore;
using PimApi.Data;
using PimApi.Repositories.Interfaces;
using PimModels.Models;

namespace PimApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly PimDbContext _context; 
    public CategoryRepository(PimDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Category category)
    {
        if (category == null) return 0;
        var info = await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return info.Entity.Id;
    }

    public async Task<List<Category>> GetAll() => await _context.Categories.ToListAsync();

    public async Task<List<Category>> GetAllInCatalog(int catalogId) => await _context.Categories.Where(c => c.CatalogId == catalogId).ToListAsync();
}
