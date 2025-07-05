using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dtos;
using ShopApi.Repositories;
using System.Security.Claims;

namespace ShopApi.Controllers;

public class FavoriteController : ControllerBase
{
  private readonly FavoriteRepository _favoriteRepository;

  public FavoriteController(FavoriteRepository favoriteRepository)
  {
    _favoriteRepository = favoriteRepository;
  }

  [Authorize]
  [HttpPost("favorite")]
  public async Task<IActionResult> AddToFavorite(int productId)
  {
    if (productId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");
      
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId == null)
      return Unauthorized();

    var userIdInt = int.Parse(userId);
    
    await _favoriteRepository.AddFavorite(userIdInt, productId);

    return Ok();
  }
}
