using Blazored.LocalStorage;
using PimModels.Models;

namespace PIM.Data.Services;

public class ProductService
{
    public string Message = "";

    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    public ProductService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    //public Task<List<Product>> GetAllInCatalog(int catalogId)
    //{

    //}
}
