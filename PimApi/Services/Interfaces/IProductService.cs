using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAll(int? catalogId);
    Task<int> CreateAsync(CreateProduct createProduct);
    Task<string> GetMainPhoto(int productId);
}
