namespace ShopApi.Utilities;

public class JwtOptions
{
  public string Key { get; set; }
  public string Issuer { get; set; }
  public int ExpiresHours { get; set; }
}
