using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Mapping;
using ShopApi.Data;
using ShopApi.Entities;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly ShopContext context;

  public CategoriesController(ShopContext context)
  {
    this.context = context;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
  public async Task<IActionResult> GetCategories()
  {
    var categories = await context.Categories
    .Select(c => c.ToDto())
    .ToListAsync();

    return Ok(categories);
  }

  [HttpGet("{categoryName}")]
  [ProducesResponseType(200, Type = typeof(Product))]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> GetProductsByCategory(string categoryName)
  {

    var products = await context.Products
    .Include(p => p.Category)
    .Include(p => p.ProductImages)
    .Where(p => p.Category.Name == categoryName)
    .Select(p => p.ToDto())
    .ToListAsync();

    if (products.Count == 0)
      return NotFound();

    return Ok(products);
  }
}
