using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.Models;
using PimModels.RequestModels;

namespace PimApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> CreateAsync(CreateProduct createProduct)
    {
        Product product = new();
        product.Name = createProduct.Name;
        product.Description = createProduct.Description;
        product.DescriptionHTML = createProduct.DescriptionHTML;
        product.CatalogId = createProduct.CatalogId;
        product.CategoryId = createProduct.CategoryId;
        product.Ean = createProduct.Ean;
        product.Sku = createProduct.Sku;
        product.Length = createProduct.Length;
        product.Width = createProduct.Width;
        product.Height = createProduct.Height;
        product.ProductAttributes = createProduct.ProductAttributes;
        product.ProductImages = new();
        createProduct.ProductImages.ForEach(x => product.ProductImages.Add(new ProductImage()
        {
            Image = string.IsNullOrEmpty(x.Image) ? null : Convert.FromBase64String(x.Image),
            ContentType = string.IsNullOrEmpty(x.ContentType) ? null : x.ContentType
        }));

        return await _productRepository.CreateAsync(product);

    }

    public async Task<List<Product>> GetAll(int? catalogId) => await _productRepository.GetAll(catalogId);

    public async Task<string> GetMainPhoto(int productId)
    {
        //var x = await _productRepository.GetMainPhoto(productId);
        //return x is null ? "" : Convert.ToBase64String(x);
        return "";
    }
}
