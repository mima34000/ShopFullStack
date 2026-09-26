using ShopFullStack.Api.Models;

namespace ShopFullStack.Api.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> ExistsAsync(int id);
}