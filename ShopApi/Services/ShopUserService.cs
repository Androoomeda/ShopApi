using ShopApi.Dtos;
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

  public async Task Register(RegisterUserDto request)
  {
    var existingUsername = await shopUserRepository.GetByUsername(request.Username);
    var existingEmail = await shopUserRepository.GetByEmail(request.Email);

    if (existingUsername != null)
      throw new FieldValidationException("username", "Такой логин уже существует");
    else if (existingEmail != null)
      throw new FieldValidationException("email", "Данная почта уже зарегистрирована");

    var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password); ;

    await shopUserRepository.Add(request.Username, request.Email, hashedPassword);
  }

  public async Task<string> Login(LoginUserDto request)
  {
    var existingUser = await shopUserRepository.GetByEmail(request.Email)
      ?? throw new FieldValidationException("email", "Неверная почта");

    var isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, existingUser.PasswordHash);

    if(!isPasswordValid)
      throw new FieldValidationException("password", "Неверный пароль");

    var token = jwtProvider.GenerateToken(existingUser);

    return token;
  }
}
