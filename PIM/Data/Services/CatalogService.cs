using Blazored.LocalStorage;
using Newtonsoft.Json;
using PimModels.Models;
using PimModels.RequestModels;
using System.Text;

namespace PIM.Data.Services;

public class CatalogService
{
    public string Message = "";

    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    public CatalogService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Catalog>> GetAllAsync()
    {
        List<Catalog> catalogs = new();

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Catalog/GetAll");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return catalogs; }

        catalogs = JsonConvert.DeserializeObject<List<Catalog>>(content);
        return catalogs;
    }

    public async Task<Catalog?> GetAsync(int id)
    {
        Catalog? catalog = new();

        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Catalog/Get/{id}");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return null; }

        catalog = JsonConvert.DeserializeObject<Catalog>(content);
        return catalog;
    }

    public async Task<int> CreateAsync(CreateCatalog createCatalog)
    {
        if (createCatalog is null)
        {
            Message = "Error. Catalog data is empty.";
            return -1;
        }

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Catalog/Create");
        request.Content = new StringContent(JsonConvert.SerializeObject(createCatalog), Encoding.UTF8, "application/json");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception)
        {
            Message = content;
            return -1;
        }

        Message = "Catalog created successfully, now You can select it on catalog list";
        return JsonConvert.DeserializeObject<int>(content);
    }

    public async Task UpdateAsync(int catalogId, UpdateCatalog updateCatalog)
    {
        if (updateCatalog is null)
        {
            Message = "Error. Catalog data is empty.";
            return;
        }

        var request = new HttpRequestMessage(HttpMethod.Post, $"/api/Catalog/Update/{catalogId}");
        request.Content = new StringContent(JsonConvert.SerializeObject(updateCatalog), Encoding.UTF8, "application/json");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception)
        {
            Message = content;
            return;
        }

        Message = "Catalog updated successfully.";
    }
}
