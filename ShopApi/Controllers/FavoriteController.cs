using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Data;
using ShopApi.Dtos;
using ShopApi.Repositories;
using ShopApi.Utilities;
using System.Security.Claims;

namespace ShopApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
  private readonly FavoriteRepository _favoriteRepository;

  public FavoriteController(FavoriteRepository favoriteRepository)
  {
    _favoriteRepository = favoriteRepository;
  }

  [HttpGet("get-ids")]
  public async Task<IActionResult> GetIds()
  {
    var userId = GetUserId();

    if (userId != null)
    {
      var favorite = await _favoriteRepository.GetFavoritesIds((int)userId);

      if (favorite == null)
        return NotFound("Favorite products not found");

      return Ok(favorite);
    }
    else
      return Unauthorized();
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Favorite>))]
  [ProducesResponseType(401)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> Get()
  {
    var userId = GetUserId();

    if (userId == null)
      return Unauthorized();

    try
    {
      var favorite = await _favoriteRepository.GetFavoritesForUser((int)userId);

      if (favorite == null)
        return NotFound("Favorite products not found");

      return Ok(favorite);
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }
  }

  [HttpPost("{productId}")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  [ProducesResponseType(401)]
  [ProducesResponseType(404)]
  [ProducesResponseType(409)]
  public async Task<IActionResult> AddToFavorite(int productId)
  {
    if (productId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var userId = GetUserId();

    if (userId == null)
      return Unauthorized();

    try
    {
      var added = await _favoriteRepository.AddFavorite((int)userId, productId);

      if (!added) return Conflict("Этот товар уже в избранном");

      return Ok();
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }
  }

  [HttpDelete("{productId}")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> RemoveFavorite(int productId)
  {
    if (productId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var userId = GetUserId();

    if (userId == null)
      return Unauthorized();

    try
    {
      await _favoriteRepository.RemoveFavorite((int)userId, productId);

      return Ok();
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }
  }


  private int? GetUserId()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (int.TryParse(userId, out int userIdInt))
      return userIdInt;

    return null;
  }
}
