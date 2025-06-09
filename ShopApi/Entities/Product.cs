namespace ShopApi.Entities;

public class Product
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required decimal Price { get; set; }
  public required string Color { get; set; }
  public string? Description { get; set; }
  public string? Image { get; set; }
}
