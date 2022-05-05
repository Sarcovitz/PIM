using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PIM.Auth;
using PIM.Data;
using PIM.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Storage
builder.Services.AddBlazoredLocalStorage();

//http
builder.Services.AddHttpClient("WebApi", httpClient => httpClient.BaseAddress = new Uri(@"https://localhost:5001"));
builder.Services.AddSingleton(service => service.GetRequiredService<IHttpClientFactory>().CreateClient("WebApi"));
builder.Services.AddHttpContextAccessor();

//auth
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());
builder.Services.AddAuthorizationCore();

//state containers
builder.Services.AddScoped<StateContainer>();

//Services
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<CurrencyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
