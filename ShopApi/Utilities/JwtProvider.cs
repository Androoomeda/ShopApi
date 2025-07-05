using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Entities;

namespace ShopApi.Utilities;

public class JwtProvider(IOptions<JwtOptions> options)
{
  private readonly JwtOptions options = options.Value;

  public string GenerateToken(ShopUser user)
  {
    Claim[] claims =
    [
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
    ];

    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
      SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      claims: claims,
      signingCredentials: signingCredentials,
      expires: DateTime.Now.AddHours(options.ExpiresHours));

    var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

    return tokenValue;
  }
}
