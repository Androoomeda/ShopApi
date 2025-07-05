using ShopApi.Entities;

namespace ShopApi.Data;

public class Favorite
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public ShopUser ShopUser { get; set; }
  public int ProductId { get; set; }
  public Product Product{ get; set; }
}
