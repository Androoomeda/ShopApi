using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Mapping;
using ShopApi.Utilities;

namespace ShopApi.Repositories;

public class FavoriteRepository(ShopContext context)
{
  private readonly ShopContext _context = context;

  public async Task<List<Favorite>> GetFavoritesIds(int userId)
  {
    var favoriteProducts = await _context.Favorites
      .AsNoTracking()
      .Where(f => f.UserId == userId)
      .ToListAsync();

    return favoriteProducts;
  }

  public async Task<List<ProductDto>> GetFavoritesForUser(int userId)
  {
    await CheckUserExists(userId);

    var favoriteProducts = await _context.Favorites
      .AsNoTracking()
      .Where(f => f.UserId == userId)
      .Include(f => f.Product)
        .ThenInclude(p => p.ProductImages)
      .Select(f => f.Product.ToDto())
      .ToListAsync();

    return favoriteProducts;
  }

  public async Task<bool> AddFavorite(int userId, int productId)
  {
    await CheckUserExists(userId);
    await CheckProductExists(productId);

    bool exists = await _context.Favorites
      .AnyAsync(f => f.UserId == userId && f.ProductId == productId);

    if (exists) return false;

    var favorite = new Favorite { UserId = userId, ProductId = productId };

    await _context.Favorites.AddAsync(favorite);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> RemoveFavorite(int userId, int productId)
  {
    await CheckUserExists(userId);
    await CheckProductExists(productId);

    var favorite = await _context.Favorites
      .FirstOrDefaultAsync(f => f.ProductId == productId && f.UserId == userId);

    if (favorite == null)
      throw new NotFoundException("Favorite not found");

    _context.Favorites.Remove(favorite);
    await _context.SaveChangesAsync();
    return true;
  }

  private async Task CheckUserExists(int userId)
  {
    var user = await _context.ShopUsers.AnyAsync(u => u.Id == userId);

    if (user == false)
      throw new NotFoundException("User not found");
  }

  private async Task CheckProductExists(int productId)
  {
    var product = await _context.Products.AnyAsync(p => p.Id == productId);

    if (product == false)
      throw new NotFoundException("Product not found");
  }
}
