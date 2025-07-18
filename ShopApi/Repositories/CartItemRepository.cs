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

  public async Task<CartInfoDto> GetCartItemsForUser(int userId)
  {
    await CheckUserExists(userId);

    var cart = await _context.Carts
      .AsNoTracking()
      .FirstOrDefaultAsync(c => c.UserId == userId) ??
      throw new NotFoundException("Cart not found");

    var cartItems = await _context.CartItems
      .AsNoTracking()
      .Where(ci => ci.CartId == cart.Id)
      .Include(ci => ci.Product)
        .ThenInclude(p => p.ProductImages)
      .Include(ci => ci.Size)
      .Select(ci => ci.ToDto())
      .ToListAsync();

    var totalQuantity = cartItems.Sum(ci => ci.Quantity);
    var totalOriginalPrice = cartItems.Sum(ci => ci.Product.Price * ci.Quantity);
    var totalDiscount = cartItems.Sum(ci => (ci.Product.Price - ci.Product.DiscountPrice) * ci.Quantity);

    decimal? totalPrice = 0;
    if (totalDiscount > 0)
      totalPrice = totalOriginalPrice - totalDiscount;
    else
      totalPrice = totalOriginalPrice;

    return new CartInfoDto
    (
      cartItems,
      totalQuantity,
      totalOriginalPrice,
      totalDiscount,
      totalPrice
    );
  }

  public async Task<CartInfoDto> AddToCart(int userId, int productId, int sizeId)
  {
    await CheckUserExists(userId);
    await CheckProductExists(productId);

    var cart = await _context.Carts
      .FirstOrDefaultAsync(c => c.UserId == userId) ??
       throw new NotFoundException("Cart not found");

    bool exists = await _context.CartItems
      .AnyAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId && ci.SizeId == sizeId);

    if (!exists)
    {
      var cartItem = new CartItem
      {
        Cart = cart,
        ProductId = productId,
        SizeId = sizeId,
        Quantity = 1
      };

      await _context.CartItems.AddAsync(cartItem);
      await _context.SaveChangesAsync();
    }

    return await GetCartItemsForUser(userId);
  }

  public async Task<CartInfoDto> EditCartItem(int userId, int quantity, int cartItemId)
  {
    await CheckUserExists(userId);

    var cartItem = await _context.CartItems
      .FirstOrDefaultAsync(ci => ci.Id == cartItemId) ??
      throw new NotFoundException("CartItem not found");

    if (quantity <= 0)
      _context.CartItems.Remove(cartItem);
    else
      cartItem.Quantity = quantity;

    await _context.SaveChangesAsync();

    return await GetCartItemsForUser(userId);
  }

  public async Task<CartInfoDto> RemoveCart(int userId, int productId)
  {
    await CheckUserExists(userId);

    var cart = await _context.Carts
      .FirstOrDefaultAsync(c => c.UserId == userId) ??
      throw new NotFoundException("Cart not found");

    var cartItem = await _context.CartItems
      .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId) ??
      throw new NotFoundException("Cart item not found");

    _context.CartItems.Remove(cartItem);
    await _context.SaveChangesAsync();

    return await GetCartItemsForUser(userId);
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
