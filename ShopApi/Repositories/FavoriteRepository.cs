using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Mapping;

namespace ShopApi.Repositories;

public class FavoriteRepository(ShopContext context)
{
  private readonly ShopContext _context = context;

  public async Task<List<ProductDto>> GetFavoritesForUser(int userId)
  {
    var favoriteProducts = await _context.Favorites
    .Where(f => f.UserId == userId)
    .Select(f => f.Product.ToDto())
    .ToListAsync();

    return favoriteProducts;
  }

  public async Task AddFavorite(int userId, int productId)
  {
    var userExists = await _context.ShopUsers.AnyAsync(u => u.Id == userId);
    var productExists = await _context.Products.AnyAsync(p => p.Id == productId);
    if (!userExists || !productExists)
      throw new Exception("User or Product not found");

    var exists = await _context.Favorites
      .AnyAsync(f => f.UserId == userId && f.ProductId == productId);

    if (exists) return;

    var favorite = new Favorite
    {
      UserId = userId,
      ProductId = productId
    };

    await _context.Favorites.AddAsync(favorite);
    await _context.SaveChangesAsync();
  }

}
