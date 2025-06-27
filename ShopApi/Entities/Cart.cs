namespace ShopApi.Entities;

public class Cart
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public ShopUser? User { get; set; }
  public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
