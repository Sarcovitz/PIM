using Blazored.LocalStorage;
using Newtonsoft.Json;
using PimModels.Models;
using PimModels.RequestModels;
using System.Text;

namespace PIM.Data.Services
{
    public class AttributeService
    {
        public string Message = "";

        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;
        public AttributeService(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
        {
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ProductAttributeProto>?> GetAllProtosAsync()
        {
            List<ProductAttributeProto>? attributeProtos = new();

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Attribute/Proto/All");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));            

            var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            try { response.EnsureSuccessStatusCode(); }
            catch (Exception) { return attributeProtos; }

            attributeProtos = JsonConvert.DeserializeObject<List<ProductAttributeProto>>(content);
            return attributeProtos;
        }

        public async Task<ProductAttributeProto?> GetProto(int id)
        {
            ProductAttributeProto? attributeProto = new();

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Attribute/Proto?id={id}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));


            var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            try { response.EnsureSuccessStatusCode(); }
            catch (Exception) { return attributeProto; }

            attributeProto = JsonConvert.DeserializeObject<ProductAttributeProto>(content);
            return attributeProto;
        }

        public async Task<int> CreateProtoAsync(CreateProductAttributeProto createProto)
        {
            if (createProto is null)
            {
                Message = "Error. Catalog data is empty.";
                return -1;
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Attribute/Proto");
            request.Content = new StringContent(JsonConvert.SerializeObject(createProto), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

            var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            try { response.EnsureSuccessStatusCode(); }
            catch (Exception)
            {
                Message = content;
                return -1;
            }

            Message = "Catalog created successfully, now You can select it on catalog list";
            return JsonConvert.DeserializeObject<int?>(content) ?? 0;
        }

        public async Task UpdateProtoAsync(int attributeId, UpdateProductAttributeProto updateProductAttributeProto)
        {
            if (updateProductAttributeProto is null)
            {
                Message = "Error. Attribute Prototype data is empty.";
                return;
            }

            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/Attribute/Proto/{attributeId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(updateProductAttributeProto), Encoding.UTF8, "application/json");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));

            var response = await _httpClientFactory.CreateClient("WebApi").SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            try { response.EnsureSuccessStatusCode(); }
            catch (Exception)
            {
                Message = content;
                return;
            }

            Message = "Attribute prototype updated successfully.";
        }
    }
}
