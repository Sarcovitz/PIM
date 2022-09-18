using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface IProductService
{
    Task<Product> GetProductAsync(int productId);
    Task<List<Product>> GetAll(int? catalogId);
    Task<int> CreateAsync(CreateProduct createProduct);
    Task<string> GetMainPhoto(int productId);
    Task<int> UpdateAsync(int productId, UpdateProduct updateProduct);
}
