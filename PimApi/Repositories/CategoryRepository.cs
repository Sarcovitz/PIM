using Microsoft.EntityFrameworkCore;
using PimApi.Data;
using PimApi.Repositories.Interfaces;
using PimModels.Models;
using PimModels.RequestModels;

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

                foreach (var attribute in attributeProtos)
                {
                    await _context.categoryProductAttributeProtos.AddAsync(new CategoryProductAttributeProto { ProductAttributeProtoId = attribute.Id, CategoryId = category.Id });
                }
                await _context.SaveChangesAsync();

                await tran.CommitAsync();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message + " " + ex.InnerException);
            }

            await _context.SaveChangesAsync();
            return category.Id;
        }
    }

    public async Task<List<Category>> GetAll() => await _context.Categories.ToListAsync();

    public async Task<List<Category>> GetAllInCatalog(int catalogId) => await _context.Categories.Where(c => c.CatalogId == catalogId).Include(x => x.SubCategories).ToListAsync();

    public async Task<Category?> GetById(int categoryId)
    {
        var fullResult = await _context.Categories.Include(c => c.AttributeProtos).ThenInclude(c => c.ProductAttributeProto).Include(x => x.SubCategories).ToListAsync();
        return fullResult.FirstOrDefault(x => x.Id == categoryId);
    }

    public async Task<int> UpdateAsync(int categoryID, UpdateCategory updateCategory)
    {
        var _category = await GetById(categoryID);
        if (_category is null) return 0;
        _category.Name = updateCategory.Name;
        _category.AttributeProtos = updateCategory.AttributeProtos;
        _category.ParentCategoryId = updateCategory.ParentCategoryId;
        _category.CatalogId = updateCategory.CatalogId;
        _context.Categories.Update(_category);

        return await _context.SaveChangesAsync();
    }
}