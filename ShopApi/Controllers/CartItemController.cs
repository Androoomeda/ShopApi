using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dtos;
using ShopApi.Repositories;
using ShopApi.Utilities;

namespace ShopApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
  private readonly CartItemRepository _cartItemRepository;

  public CartItemController(CartItemRepository cartItemRepository)
  {
    _cartItemRepository = cartItemRepository;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<CartInfoDto>))]
  [ProducesResponseType(401)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> Get()
  {
    var userId = GetUserId();

    if (userId == null)
      return Unauthorized();

    try
    {
      var carItems = await _cartItemRepository.GetCartItemsForUser((int)userId);

      if (carItems == null)
        return NotFound("Cart items not found");

      return Ok(carItems);
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }
  }

  [HttpPost("AddToCart")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  [ProducesResponseType(401)]
  [ProducesResponseType(404)]
  [ProducesResponseType(409)]
  public async Task<IActionResult> AddToCart([FromBody] NewCartItem newItem)
  {
    if (newItem.ProductId <= 0 || newItem.ProductId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var userId = GetUserId();

    if (userId == null)
      return Unauthorized();

    try
    {
      var data = await _cartItemRepository.AddToCart((int)userId, newItem.ProductId, newItem.SizeId);

      return Ok(data);
    }
    catch (NotFoundException ex)
    {
      return NotFound(ex.Message);
    }
  }

  [HttpPut("{cartItemId}")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  [ProducesResponseType(401)]
  public async Task<IActionResult> EditCartItem([FromBody] UpdateCartItemDto cartItem, int cartItemId)
  {
    if (cartItemId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var userId = GetUserId();

    if (userId == null)
      return Unauthorized();

    try 
    {
      var data = await _cartItemRepository.EditCartItem((int)userId, cartItem.Quantity, cartItemId);

      return Ok(data);
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
  public async Task<IActionResult> RemoveCartItem(int productId)
  {
    if (productId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var userId = GetUserId();

    if (userId == null)
      return Unauthorized();

    try
    {
      var data = await _cartItemRepository.RemoveCart((int)userId, productId);

      return Ok(data);
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