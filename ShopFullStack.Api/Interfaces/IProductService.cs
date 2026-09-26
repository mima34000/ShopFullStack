using ShopFullStack.Api.Dtos;

namespace ShopFullStack.Api.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<bool> UpdateAsync(int id, CreateProductDto dto);
    Task<bool> DeleteAsync(int id);
}