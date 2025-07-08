using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Entities;
using ShopApi.Mapping;
using ShopApi.Utilities;

namespace ShopApi.Repositories;

public class CartItemRepository(ShopContext context)
{
  private readonly ShopContext _context = context;

  public async Task<List<ProductDto>> GetCartItemsForUser(int userId)
  {
    await CheckUserExists(userId);

    var cart = await _context.Carts
      .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
      throw new NotFoundException("Cart not found");

    var cartItems = await _context.CartItems
        .Where(ci => ci.CartId == cart.Id)
        .Include(ci => ci.Product)
        .Include(ci => ci.Product.ProductImages)
        .Select(ci => ci.Product.ToDto())
        .ToListAsync();

    return cartItems;
  }

  public async Task<bool> AddToCart(int userId, int productId, int sizeId)
  {
    await CheckUserExists(userId);
    await CheckProductExists(productId);

    var cart = await _context.Carts
      .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
      throw new NotFoundException("Cart not found");

    bool exists = await _context.CartItems
      .AnyAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);

    if (exists) return false;

    var cartItem = new CartItem
    {
      Cart = cart,
      ProductId = productId,
      SizeId = sizeId,
      Quantity = 1
    };

    await _context.CartItems.AddAsync(cartItem);
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> RemoveCart(int userId, int productId)
  {
    await CheckUserExists(userId);
    await CheckProductExists(productId);

    var cart = await _context.Carts
      .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
      throw new NotFoundException("Cart not found");

    var cartItem = await _context.CartItems
      .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);

    if (cartItem == null)
      return false;

    _context.CartItems.Remove(cartItem);
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
