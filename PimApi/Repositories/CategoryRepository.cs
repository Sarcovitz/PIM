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

    public async Task<int> CreateAsync(Category category, List<ProductAttributeProto>? attributeProtos)
    {
        if (category == null) return 0;

        using (var tran = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                foreach(var attribute in attributeProtos)
                {
                    await _context.categoryProductAttributeProtos.AddAsync(new CategoryProductAttributeProto { ProductAttributeProtoId = attribute.Id, CategoryId = category.Id });
                }
                await _context.SaveChangesAsync();

                await tran.CommitAsync();
            }
            catch(Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message +" "+ ex.InnerException);
            }
            
            await _context.SaveChangesAsync();
            return category.Id;
        }
    }

    public async Task<List<Category>> GetAll() => await _context.Categories.ToListAsync();

    public async Task<List<Category>> GetAllInCatalog(int catalogId) => await _context.Categories.Where(c => c.CatalogId == catalogId).ToListAsync();

    public async Task<Category?> GetById(int categoryId) => await _context.Categories.Include(c => c.AttributeProtos).ThenInclude(c => c.ProductAttributeProto).FirstOrDefaultAsync(c => c.Id == categoryId);
}
