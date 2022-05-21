using Blazored.LocalStorage;
using Newtonsoft.Json;
using PimModels.Models;

namespace PIM.Data.Services;

public class CategoryService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Category>> GetCategoriesAsync(int? catalogId = null)
    {
        List<Category> categories = new();

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Catalog/GetAll");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return categories; }

        categories = JsonConvert.DeserializeObject<List<Category>>(content);
        return categories;
    }
}
