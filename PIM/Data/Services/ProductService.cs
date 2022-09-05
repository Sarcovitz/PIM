using Blazored.LocalStorage;
using Newtonsoft.Json;
using PimModels.Models;
using PimModels.RequestModels;
using System.Text;

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

    public async Task<int> CreateAsync(CreateProduct createProduct)
    {
        if (createProduct is null)
        {
            Message = "Error. Product data is empty.";
            return -1;
        }

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Product");
        request.Content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception)
        {
            Message = content;
            return -1;
        }

        Message = "Product created successfully, now You can select it on product list";
        return JsonConvert.DeserializeObject<int>(content);
    }

    //public async Task<string> GetMainImage(int prodId)
    //{
        
    //}
}
