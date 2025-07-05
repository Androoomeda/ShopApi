using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShopApi.Dtos;
using ShopApi.Services;
using ShopApi.Utilities;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopUserController : ControllerBase
{
  private readonly ShopUserService _shopUserService;
  private readonly JwtOptions _options;

  public ShopUserController(ShopUserService shopUserService, IOptions<JwtOptions> options)
  {
    _shopUserService = shopUserService;
    _options = options.Value;
  }

  [HttpPost("register")]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
  {
    try
    {
      await _shopUserService.Register(request);
      return Ok();
    }
    catch (FieldValidationException ex)
    {
      return BadRequest(new { field = ex.Field, message = ex.Message });
    }
    catch (Exception)
    {
      return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUserDto request)
  {
    try
    {
      var token = await _shopUserService.Login(request);

      Response.Cookies.Append("tasty-cookies", token, new CookieOptions
      {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = DateTimeOffset.UtcNow.AddHours(_options.ExpiresHours)
      });

      return Ok(new { token = token });
    }
    catch (FieldValidationException ex)
    {
      return BadRequest(new { field = ex.Field, message = ex.Message });
    }
    catch (Exception)
    {
      return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
    }
  }
}
