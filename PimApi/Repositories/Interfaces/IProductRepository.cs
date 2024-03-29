﻿using PimModels.Models;

namespace PimApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetById(int id);
    Task<Product?> GetByName(string name, int? catalogId);
    Task<Product?> GetBySku(string sku, int? catalogId);
    Task<Product?> GetByEan(string ean, int? catalogId);
    Task<List<Product>> GetAll(int? catalogId);
    Task<int> CreateAsync(Product product);
    Task<ProductImage?> GetMainPhoto(int productId);
    Task<int> UpdateAsync(Product product);
}
