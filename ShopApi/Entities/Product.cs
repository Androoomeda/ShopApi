using ShopApi.Data;

namespace ShopApi.Entities;

public class Product
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required string Color { get; set; }
  public required int CategoryId { get; set; }
  public required Category Category { get; set; }
  public required decimal Price { get; set; }
  public decimal? DiscountPrice { get; set; }
  public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
  public ICollection<ShopUser> UsersWhoFavorited { get; set; } = new List<ShopUser>();
  public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
  public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
