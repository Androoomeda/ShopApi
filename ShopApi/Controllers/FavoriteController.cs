using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dtos;
using ShopApi.Repositories;
using System.Security.Claims;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
  private readonly FavoriteRepository _favoriteRepository;

  public FavoriteController(FavoriteRepository favoriteRepository)
  {
    _favoriteRepository = favoriteRepository;
  }

  [Authorize]
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var userId = GetUserId();

    if (userId != null)
    {
      var favorite = await _favoriteRepository.GetFavoritesForUser((int)userId);

      if (favorite == null)
        return NotFound("Favorite products not found");

      return Ok(favorite);
    }
    else
      return Unauthorized();
  }

  [Authorize]
  [HttpPost("{productId}")]
  public async Task<IActionResult> AddToFavorite(int productId)
  {
    if (productId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var userId = GetUserId();

    if (userId != null)
    {
      await _favoriteRepository.AddFavorite((int)userId, productId);

      return Ok();
    }
    else
      return Unauthorized();
  }

  private int? GetUserId()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId != null)
    {
      var userIdInt = int.Parse(userId);
      return userIdInt;
    }
    else return null;
  }
}
