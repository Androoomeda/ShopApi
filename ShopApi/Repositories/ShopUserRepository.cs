using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Entities;

namespace ShopApi.Repositories;

public class ShopUserRepository
{
  private readonly ShopContext context;

  public ShopUserRepository(ShopContext context)
  {
    this.context = context;
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

    await context.ShopUsers.AddAsync(newUser);
    await context.SaveChangesAsync();
  }

  public async Task<ShopUser> GetByEmail(string email)
  {
    var user = await context.ShopUsers
    .AsNoTracking()
    .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");

    return user;
  }
}
