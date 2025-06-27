using Microsoft.AspNetCore.Mvc;
using ShopApi.Dtos;
using ShopApi.Services;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopUserController() : ControllerBase
{
  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterUserRequest request, ShopUserService shopUserService)
  {
    await shopUserService.Register(request.Username, request.Email, request.Password);

    return Ok();
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(
    [FromBody] LoginUserRequest request,
    ShopUserService shopUserService)
  {
    var token = await shopUserService.Login(request.Email, request.Password);

    Response.Cookies.Append("tasty-cookies", token);

    return Ok();
  }
}
