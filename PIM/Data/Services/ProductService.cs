using Blazored.LocalStorage;
using Newtonsoft.Json;
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

    public async Task<List<Product>> GetAll(int? catalogId = null)
    {
        List<Product>? products = new();

        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Product/All?catalogId={catalogId}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch { return products; }

        return JsonConvert.DeserializeObject<List<Product>>(content) ?? new List<Product>();
    }
}
