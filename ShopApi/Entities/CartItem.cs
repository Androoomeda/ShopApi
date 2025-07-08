namespace ShopApi.Entities;

public class CartItem
{
  public int Id { get; set; }
  public int CartId { get; set; }
  public Cart Cart { get; set; }
  public int ProductId { get; set; }
  public Product Product { get; set; }
  public int SizeId { get; set; }
  public Size Size { get; set; }
  public required int Quantity { get; set; }
}
