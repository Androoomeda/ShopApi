using Microsoft.AspNetCore.Mvc;
using ShopApi.Dtos;
using ShopApi.Services;
using ShopApi.Utilities;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopUserController : ControllerBase
{
  private readonly ShopUserService _shopUserService;

  public ShopUserController(ShopUserService shopUserService)
  {
    _shopUserService = shopUserService;
  }

  [HttpPost("register")]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
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
  public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
  {
    var token = await _shopUserService.Login(request);

    Response.Cookies.Append("tasty-cookies", token);

    return Ok();
  }
}
