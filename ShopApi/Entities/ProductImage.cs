namespace ShopApi.Entities;

public class ProductImage
{
  public int Id { get; set; }
  public required int ProductId { get; set; }
  public required Product Product { get; set; }
  public required string ImagePath { get; set; }
}
