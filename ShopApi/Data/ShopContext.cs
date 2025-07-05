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
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Product>()
      .HasOne(p => p.Category)
      .WithMany(c => c.Products)
      .HasForeignKey(p => p.CategoryId);

    modelBuilder.Entity<ProductImage>()
      .HasOne(pi => pi.Product)
      .WithMany(p => p.ProductImages)
      .HasForeignKey(pi => pi.ProductId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<CartItem>()
      .HasOne(ci => ci.Size)
      .WithMany(s => s.CartItems)
      .HasForeignKey(ci => ci.SizeId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<CartItem>()
      .HasOne(ci => ci.Product)
      .WithMany(p => p.CartItems)
      .HasForeignKey(ci => ci.ProductId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<CartItem>()
      .HasOne(ci => ci.Cart)
      .WithMany(c => c.CartItems)
      .HasForeignKey(ci => ci.CartId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<ShopUser>()
      .HasMany(u => u.FavoriteProducts)
      .WithMany(p => p.UsersWhoFavorited)
      .UsingEntity(f => f.ToTable("Favorites"));
  }
}
