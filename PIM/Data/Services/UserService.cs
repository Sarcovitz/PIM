using Blazored.LocalStorage;
using Newtonsoft.Json;
using PimModels.Models;

namespace PIM.Data.Services;

public class UserService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    public UserService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<User?> GetUserByNameAsync(string username)
    {
        var user = new User();

        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/User/Get?username={username}");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));        

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return null; }

        user = JsonConvert.DeserializeObject<User>(content);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = new User();

        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/User/Get?id={id}");
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return null; }

        user = JsonConvert.DeserializeObject<User>(content);
        return user;
    }
}
