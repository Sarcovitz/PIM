using Blazored.LocalStorage;
using PimModels.DTO;
using PimModels.RequestModels;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using PIM.Data;
using PimModels.Models;

namespace PIM.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILocalStorageService _localStorage;
    private readonly StateContainer _stateContainer;

    public AuthStateProvider(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage, StateContainer stateContainer)
    {
        _httpClientFactory = httpClientFactory;
        _localStorage = localStorage;
        _stateContainer = stateContainer;
    }

    public string Message { get; set; }

    private string token;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() => await GenerateAuthenticationState(await GetToken());

    public async Task LoginAsync(LoginUser loginUser)
    {
        Message = "";
        await SetToken(loginUser);
        NotifyAuthenticationStateChanged(GenerateAuthenticationState(token));
    }

    public async Task LogoutAsync()
    {
        await SetToken(null);
        NotifyAuthenticationStateChanged(GenerateAuthenticationState(token));
    }

    public async Task<bool> RegisterAsync(RegisterUser registerUser)
    {
        if (registerUser is null)
        {
            Message = "Error. User data is empty.";
            return false;
        }
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Authentication/Register");
        request.Content = new StringContent(JsonConvert.SerializeObject(registerUser), Encoding.UTF8, "application/json");

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) 
        {
            Message = content;
            return false;
        }

        Message = "User registered successfully, now You can sign in to the application.";
        return true;
    }

    public async Task<string> GetToken()
    {
        if (await IsTokenValid()) return token;

        LoginUser? user = await _localStorage.GetItemAsync<LoginUser>("loginUser");
        if(user is null) return "";

        try { return await RefreshToken(user); }
        catch
        {
            await LogoutAsync();
            return "";
        }
    }

    private async Task<bool> IsTokenValid()
    {
        if(string.IsNullOrWhiteSpace(token)) return false;
        if (DateTimeOffset.FromUnixTimeSeconds(await _localStorage.GetItemAsync<long>("tokenExpirationDate")) <= DateTime.Now.AddMinutes(30)) return false;

        return true;
    }
    
    private async Task<string> RefreshToken(LoginUser? loginUser)
    {
        if (loginUser is null) return "";
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/Authentication/Login");
        request.Content = new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json");

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) 
        {
            Message = content;
            return token = ""; 
        }

        LoginUserDTO fetchedData = JsonConvert.DeserializeObject<LoginUserDTO>(content);
        await _localStorage.SetItemAsync<int>("userId", fetchedData.UserId);

        return token = fetchedData.Token;
    }

    private async Task SetToken(LoginUser? loginUser)
    {
        if (loginUser is null) await _localStorage.ClearAsync();
        await RefreshToken(loginUser);
        await _localStorage.SetItemAsync("token", token);
        await _localStorage.SetItemAsync("loginUser", loginUser);
    }

    private Task<AuthenticationState> GenerateAuthenticationState(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        GetCurrentUser();

        return Task.FromResult(state);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var payload = token.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    public async void GetCurrentUser()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/Authentication/CurrentUser");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

        var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        try { response.EnsureSuccessStatusCode(); }
        catch (Exception) { return; }

        User? user = JsonConvert.DeserializeObject<User>(content);

        await _stateContainer.SetUser(user);
    }
}
