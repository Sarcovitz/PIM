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

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.Where(x => x.Id == id).Include(x => x.ProductAttributes).ThenInclude(x=>x.AttributeProto).Include(x => x.ProductImages).FirstOrDefaultAsync();
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

    public async Task<int> UpdateAsync(Product model)
    {
        var _product = await GetById(model.Id);
        if (_product is null) return 0;
        _product.Name = model.Name;
        _product.Sku = model.Sku;
        _product.Ean = model.Ean;
        _product.CatalogId = model.CatalogId;
        _product.CategoryId = model.CategoryId;
        _product.Description = model.Description;
        _product.DescriptionHTML = model.DescriptionHTML;
        _product.Width = model.Width;
        _product.Height = model.Height;
        _product.Length = model.Length;
        _product.ProductImages = model.ProductImages;
        _product.ProductAttributes = model.ProductAttributes;

        _context.Products.Update(_product);

        return await _context.SaveChangesAsync();
    }
}
