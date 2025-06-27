using Microsoft.EntityFrameworkCore;
using ShopApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShopApi.Data;

public class ShopContext(DbContextOptions<ShopContext> options) 
  : DbContext(options)
{

  public DbSet<Product> Products => Set<Product>();
  public DbSet<ProductImage> ProductImages => Set<ProductImage>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<Size> Sizes => Set<Size>();
  public DbSet<Favorite> Favorites => Set<Favorite>();
  public DbSet<ShopUser> ShopUsers => Set<ShopUser>();
  public DbSet<Cart> Carts => Set<Cart>();
  public DbSet<CartItem> CartItems => Set<CartItem>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ShopUser>(entity => entity.ToTable("Users"));

    modelBuilder.Entity<ShopUser>()
        .HasOne(u => u.Cart)
        .WithOne(c => c.User)
        .HasForeignKey<Cart>(c => c.UserId)
        .IsRequired();

    modelBuilder.Entity<Product>()
      .HasOne(p => p.Category)
      .WithMany(c => c.Products)
      .HasForeignKey(p => p.CategoryId);

    modelBuilder.Entity<ProductImage>()
      .HasOne(pi => pi.Product)
      .WithMany(p => p.ProductImages)
      .HasForeignKey(pi => pi.ProductId);

    modelBuilder.Entity<CartItem>()
    .HasOne(ci => ci.Size)
    .WithMany(s => s.CartItems)
    .HasForeignKey(ci => ci.SizeId);
  }
}
