using PimModels.Models;

namespace PimApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetById(int id, int? catalogId);
    Task<Product?> GetByName(string name, int? catalogId);
    Task<Product?> GetBySku(string sku, int? catalogId);
    Task<Product?> GetByEan(string ean, int? catalogId);
    Task<List<Product>> GetAll(int? catalogId);
}
