using Microsoft.EntityFrameworkCore;
using ShopFullStack.Api.Data;
using ShopFullStack.Api.Interfaces;
using ShopFullStack.Api.Models;

namespace ShopFullStack.Api.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id);
    }
}