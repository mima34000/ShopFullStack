using ShopFullStack.Api.Dtos;

namespace ShopFullStack.Web.Services;

public class CategoryApiClient
{
    private readonly HttpClient _httpClient;

    public CategoryApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories");
        return result ?? new List<CategoryDto>();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CategoryDto>($"api/categories/{id}");
    }

    public async Task<CategoryDto?> CreateAsync(CreateCategoryDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/categories", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CategoryDto>();
    }
}