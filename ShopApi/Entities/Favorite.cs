namespace ShopApi.Entities;

public class Favorite
{
  public int Id { get; set; }
  public required ShopUser User { get; set; }
  public required Product Product { get; set; }
}
