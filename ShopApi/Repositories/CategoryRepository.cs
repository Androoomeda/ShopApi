using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Mapping;

namespace ShopApi.Repositories;

public class CategoryRepository(ShopContext context)
{
  private readonly ShopContext _context = context;

  public async Task<List<CategoryDto>> Get()
  {
    var categories = await _context.Categories
    .Select(c => c.ToDto())
    .ToListAsync();

    return categories;
  }

  public async Task<List<ProductDto>> GetProductsByCategoryName(string categoryName)
  {
    var products = await _context.Products
    .Include(p => p.Category)
    .Include(p => p.ProductImages)
    .Where(p => p.Category.Name == categoryName)
    .Select(p => p.ToDto())
    .ToListAsync();

    return products;
  }
}
