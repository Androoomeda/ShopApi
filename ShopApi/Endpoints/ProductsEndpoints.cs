namespace ShopApi.Endpoints;

using ShopApi.Data;
using Microsoft.EntityFrameworkCore;
using ShopApi.Mapping;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Entities;


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
      .Select(p => p.ToDto())
      .ToListAsync();

    return Ok(products);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(200, Type = typeof(Product))]
  [ProducesResponseType(400)]
  public async Task<IActionResult> GetById(int id)
  {
    var product = await context.Products
      .Include(p => p.Category)
      .Where(p => p.Id == id)
      .Select(p => p.ToDto())
      .FirstOrDefaultAsync();

    if (product == null)
      return NotFound();


    return Ok(product);
  }
}
