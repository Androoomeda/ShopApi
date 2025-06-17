namespace ShopApi.Entities;

public class Size
{
  public int Id { get; set; }
  public required string Label { get; set; }
  public required string SizeType { get; set; }
  public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
