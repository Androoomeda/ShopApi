using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dtos;
using ShopApi.Extensions;
using ShopApi.Repositories;
using ShopApi.Utilities;

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
  [ProducesResponseType(200)]
  [ProducesResponseType(401)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> GetIds()
  {
    var userId = User.GetUserId();

    if (userId == null)
      return Unauthorized();

    var favorite = await _favoriteRepository.GetFavoritesIds((int)userId);

    if (favorite == null)
      return NotFound("Favorite products not found");

    return Ok(favorite);
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
  [ProducesResponseType(401)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> Get()
  {
    var userId = User.GetUserId();

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

    var userId = User.GetUserId();

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
  [ProducesResponseType(401)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> RemoveFavorite(int productId)
  {
    if (productId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var userId = User.GetUserId();

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
}
