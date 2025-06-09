using Microsoft.EntityFrameworkCore;
using ShopApi.Entities;

namespace ShopApi.Data;

public class ShopContext(DbContextOptions<ShopContext> options)
  : DbContext(options)
{
  public DbSet<Product> Products => Set<Product>();
}
