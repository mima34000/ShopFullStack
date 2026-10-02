using ShopFullStack.Api.Dtos;

namespace ShopFullStack.Web.Services;

public class ProductApiClient
{
    private readonly HttpClient _httpClient;

    public ProductApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/products");
        return result ?? new List<ProductDto>();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{id}");
    }

    public async Task<ProductDto?> CreateAsync(CreateProductDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }

    public async Task UpdateAsync(int id, CreateProductDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/products/{id}");
        response.EnsureSuccessStatusCode();
    }
}