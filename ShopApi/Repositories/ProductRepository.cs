using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Repositories;

public class ProductRepository
{
  private readonly ShopContext _context;

  public ProductRepository(ShopContext context)
  {
    _context = context;
  }

  public async Task<List<ProductDto>> Get()
  {
    var products = await _context.Products
    .Include(p => p.Category)
    .Include(p => p.ProductImages)
    .Select(p => p.ToDto())
    .ToListAsync();

    return products;
  }

  public async Task<ProductDetailsDto> GetById(int id)
  {
    var product = await _context.Products
    .Include(p => p.Category)
    .Include(p => p.ProductImages)
    .Where(p => p.Id == id)
    .FirstOrDefaultAsync() ?? throw new Exception("Product not found");;

    List<string> sizesLabel = new ();
    if (product != null)
    {
      sizesLabel = await _context.Sizes
      .Where(s => s.SizeType == product.Category.SizeType)
      .Select(s => s.Label)
      .ToListAsync();
    }

    return product.ToDetailsDto(sizesLabel);
  }
}
