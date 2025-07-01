using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Entities;

namespace ShopApi.Repositories;

public class ShopUserRepository
{
  private readonly ShopContext _context;

  public ShopUserRepository(ShopContext context)
  {
    this._context = context;
  }

  public async Task Add(string username, string email, string hashedPassword)
  {
    var newUser = new ShopUser
    {
      Username = username,
      Email = email,
      PasswordHash = hashedPassword,
    };

    var cart = new Cart
    {
      User = newUser,
      UserId = newUser.Id
    };

    newUser.Cart = cart;

    await _context.ShopUsers.AddAsync(newUser);
    await _context.SaveChangesAsync();
  }

  public async Task<ShopUser> GetByEmail(string email)
  {
    var user = await _context.ShopUsers
    .AsNoTracking()
    .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");

    return user;
  }
}
