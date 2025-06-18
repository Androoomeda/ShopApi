using System.Text.Json.Serialization;

namespace ShopApi.Entities;

public class Category
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string SizeType { get; set; }

  [JsonIgnore]
  public ICollection<Product> Products { get; set; } = new List<Product>();
}
