using Blazored.LocalStorage;
using Newtonsoft.Json;
using PimModels.Models;
using PimModels.RequestModels;
using System.Text;

namespace PIM.Data.Services;

public class CategoryService
{
    public string Message = "";

    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Category>> GetAllAsync(int? catalogId = null)
    {
        List<Category>? categories = new();

        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Category/All?catalogId={catalogId}");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return categories; }

        categories = JsonConvert.DeserializeObject<List<Category>>(content);
        return categories ?? new List<Category>();
    }

    public async Task<Category?> GetAsync(int id)
    {
        Category? category = new();

        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Category/{id}");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return null; }

        category = JsonConvert.DeserializeObject<Category>(content);
        return category;
    }

    public async Task<int> CreateAsync(CreateCategory createCategory)
    {
        if (createCategory is null)
        {
            Message = "Error. Category data is empty.";
            return -1;
        }

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Category");
        request.Content = new StringContent(JsonConvert.SerializeObject(createCategory), Encoding.UTF8, "application/json");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception)
        {
            Message = content;
            return -1;
        }

        Message = "Category created successfully, now You can see it on category list";
        return JsonConvert.DeserializeObject<int>(content);
    }

    public async Task<bool> UpdateAsync(UpdateCategory update)
    {
        return true;
    }
}
