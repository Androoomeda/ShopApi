using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Mapping;
using Microsoft.EntityFrameworkCore;
using ShopApi.Utilities;

namespace ShopApi.Repositories;

public class ProductRepository(ShopContext context)
{
  private readonly ShopContext _context = context;

  public async Task<List<ProductDto>> Get()
  {
    var products = await _context.Products
    .Include(p => p.ProductImages)
    .Select(p => p.ToDto())
    .ToListAsync();

    return products;
  }

  public async Task<ProductDetailsDto> GetById(int id, int? userId = null)
  {
    var product = await _context.Products
    .Include(p => p.Category)
    .Include(p => p.ProductImages)
    .Where(p => p.Id == id)
    .FirstOrDefaultAsync();

    if (product == null)
      throw new NotFoundException("Product not found");

    List<SizeDto> sizes = new();

    sizes = await _context.Sizes
      .Where(s => s.SizeType == product.Category.SizeType)
      .Select(s => s.ToDto())
      .ToListAsync();


    bool isFavorite = false;
    bool isInCart = false;
    if (userId.HasValue)
    {
      isFavorite = await _context.Favorites
        .AnyAsync(f => f.UserId == userId && f.ProductId == product.Id);

      var cart = await _context.Carts
           .FirstOrDefaultAsync(c => c.UserId == userId);

      if (cart != null)
        isInCart = await _context.CartItems
         .AnyAsync(ci => ci.CartId == cart.Id && ci.ProductId == product.Id);
    }

    return product.ToDetailsDto(sizes, isFavorite, isInCart);
  }
}
