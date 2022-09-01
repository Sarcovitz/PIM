using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.Models;

namespace PimApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAll(int? catalogId) => await _productRepository.GetAll(catalogId);
}
