using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Mapping;
using ShopApi.Data;
using ShopApi.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ShopApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
  private readonly ShopContext context;

  public ProductsController(ShopContext context)
  {
    this.context = context;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
  public async Task<IActionResult> GetProducts()
  {
    var products = await context.Products
    .Include(p => p.Category)
    .Include(p => p.ProductImages)
    .Select(p => p.ToDto())
    .ToListAsync();

    return Ok(products);
  }

  [HttpGet("{productId}")]
  [ProducesResponseType(200, Type = typeof(Product))]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> GetProductById(int productId)
  {
    if (productId <= 0)
      return BadRequest("Id не может быть меньше или равен 0");

    var product = await context.Products
    .Include(p => p.Category)
    .Include(p => p.ProductImages)
    .Where(p => p.Id == productId)
    .FirstOrDefaultAsync();


    List<string> sizesLabel;
    if (product != null)
    {
      sizesLabel = await context.Sizes
      .Where(s => s.SizeType == product.Category.SizeType)
      .Select(s => s.Label)
      .ToListAsync();
    }
    else
      return NotFound();

    return Ok(product.ToDetailsDto(sizesLabel));
  }

  [HttpPost("favorite")]
  [Authorize]
  public async Task<IActionResult> AddProductToFavorite()
  {
    return Ok("something");
  }

  // [HttpPost]
  // TODO: сделать API для админа

  // [HttpPut]

  // [HttpDelete]
}
