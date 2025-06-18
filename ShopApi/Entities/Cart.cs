namespace ShopApi.Entities;

public class Cart
{
  public int Id { get; set; }
  public int UserId  { get; set; }
  public required User User { get; set; }
  public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
