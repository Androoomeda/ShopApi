using Microsoft.AspNetCore.Identity;

namespace ShopApi.Entities;

public class ShopUser
{
  public int Id { get; set; }
  public string Username { get; set; }
  public string Email { get; set; }
  public string PasswordHash { get; set; }

  public Cart? Cart { get; set; }
  public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
