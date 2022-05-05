using Blazored.LocalStorage;
using Newtonsoft.Json;
using PimModels.Models;

namespace PIM.Data.Services;

public class CurrencyService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    public CurrencyService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Currency>> GetAllAsync()
    {
        var currencies = new List<Currency>();

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Currency/GetAll");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return currencies; }

        currencies = JsonConvert.DeserializeObject<List<Currency>>(content);
        return currencies;
    }

}
