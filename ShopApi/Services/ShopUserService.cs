using ShopApi.Repositories;
using ShopApi.Utilities;

namespace ShopApi.Services;

public class ShopUserService
{
  private readonly ShopUserRepository shopUserRepository;
  private readonly JwtProvider jwtProvider;

  public ShopUserService(ShopUserRepository shopUserRepository,  JwtProvider jwtProvider)
  {
    this.shopUserRepository = shopUserRepository;
    this.jwtProvider = jwtProvider;
  }

  public async Task Register(string username, string email, string password)
  {
    var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);;

    await shopUserRepository.Add(username, email, hashedPassword);
  }

  public async Task<string> Login(string email, string password)
  {
    var user = await shopUserRepository.GetByEmail(email);
    var result = BCrypt.Net.BCrypt.EnhancedVerify(password, user.PasswordHash);

    if(!result)
      throw new Exception("Failed to login");

    var token = jwtProvider.GenerateToken(user);

    return token;
  }
}
