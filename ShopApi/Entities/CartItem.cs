namespace ShopApi.Entities;

public class CartItem
{
  public int Id { get; set; }
  public required Cart Cart { get; set; }
  public required Product Product { get; set; }
  public required Size Size { get; set; }
  public required int Quantity { get; set; }
}
