namespace ShopApi.Entities;

public class User
{
  public int Id { get; set; }
  public required string Username { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required Cart Cart { get; set; }
  public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
