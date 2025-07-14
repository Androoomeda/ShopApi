using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Entities;
using ShopApi.Utilities;

namespace ShopApi.Repositories;

public class ShopUserRepository(ShopContext context)
{
  private readonly ShopContext _context = context;

  public async Task Add(string username, string email, string hashedPassword)
  {
    var newUser = new ShopUser
    {
      Username = username,
      Email = email,
      PasswordHash = hashedPassword,
      Cart = new Cart()
    };

    await _context.ShopUsers.AddAsync(newUser);
    await _context.SaveChangesAsync();
  }

  public async Task<ShopUser?> GetByEmail(string email)
  {
    var user = await _context.ShopUsers
    .AsNoTracking()
    .FirstOrDefaultAsync(u => u.Email == email);

    return user;
  }

  public async Task<ShopUser?> GetByUsername(string username)
  {
    var user = await _context.ShopUsers
    .AsNoTracking()
    .FirstOrDefaultAsync(u => u.Username == username);

    return user;
  }

  public async Task<UserInfoDto> GetUserInfo(int userId)
  {
    var user = await _context.ShopUsers
    .AsNoTracking()
    .FirstOrDefaultAsync(u => u.Id == userId);

    if (user == null)
      throw new NotFoundException("Пользователь не найден");

    int favoriteCount = _context.Favorites.Where(f => f.UserId == user.Id).Count();
    int cartItemsCount = _context.CartItems.Where(ci => ci.Cart.UserId == user.Id).Sum(ci => ci.Quantity);

    return new UserInfoDto
    (
      user.Username,
      user.Email,
      favoriteCount,
      cartItemsCount
    );
  }
}
