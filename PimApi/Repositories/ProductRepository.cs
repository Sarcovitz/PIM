using Microsoft.EntityFrameworkCore;
using PimApi.Data;
using PimApi.Repositories.Interfaces;
using PimModels.Models;

namespace PimApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PimDbContext _context;

    public ProductRepository(PimDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Product product)
    {
        try
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        catch{ }

        return product.Id;
    }

    public async Task<List<Product>> GetAll(int? catalogId)
    {
        if (catalogId.HasValue) return await _context.Products.Where(p => p.CatalogId == catalogId).ToListAsync();
        else return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByEan(string ean, int? catalogId)
    {
        if (catalogId.HasValue) return await _context.Products.FirstOrDefaultAsync(p => p.Ean == ean && p.CatalogId == catalogId);
        else return await _context.Products.FirstOrDefaultAsync(p => p.Ean == ean);
    }

    public async Task<Product?> GetById(int id, int? catalogId)
    {
        if (catalogId.HasValue) return await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.CatalogId == catalogId);
        else return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetByName(string name, int? catalogId)
    {
        if (catalogId.HasValue) return await _context.Products.FirstOrDefaultAsync(p => p.Sku == name && p.CatalogId == catalogId);
        else return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<Product?> GetBySku(string sku, int? catalogId)
    {
        if (catalogId.HasValue) return await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku && p.CatalogId == catalogId);
        else return await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public async Task<ProductImage?> GetMainPhoto(int productId) => _context.ProductImages.FirstOrDefault(x => x.ProductId == productId);
}
