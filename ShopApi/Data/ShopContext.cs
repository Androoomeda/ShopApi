using Microsoft.EntityFrameworkCore;
using ShopApi.Entities;

namespace ShopApi.Data;

public class ShopContext(DbContextOptions<ShopContext> options)
  : DbContext(options)
{
  public DbSet<Product> Products => Set<Product>();
  public DbSet<ProductImage> ProductImages => Set<ProductImage>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<Size> Sizes => Set<Size>();
  public DbSet<Favorite> Favorites => Set<Favorite>();
  public DbSet<User> Users => Set<User>();
  public DbSet<Cart> Carts => Set<Cart>();
  public DbSet<CartItem> CartItems => Set<CartItem>();
}
