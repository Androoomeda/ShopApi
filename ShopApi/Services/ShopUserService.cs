using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ShopApi.Data;
using ShopApi.Entities;
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
    var hashedPassword = Generate(password);

    await shopUserRepository.Add(username, email, hashedPassword);
  }

  public async Task<string> Login(string email, string password)
  {
    var user = await shopUserRepository.GetByEmail(email);
    var result = VerifyPassword(password, user.PasswordHash);

    if(!result)
      throw new Exception("Failed to login");

    var token = jwtProvider.GenerateToken(user);

    return token;
  }

  public string Generate(string password)
  {
    return  BCrypt.Net.BCrypt.EnhancedHashPassword(password);
  }

  public bool VerifyPassword(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
  }
}
