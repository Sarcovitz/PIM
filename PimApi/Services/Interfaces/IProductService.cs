using PimModels.Models;

namespace PimApi.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAll(int? catalogId);
}
