using Microsoft.AspNetCore.Mvc;
using ShopApi.Entities;
using ShopApi.Repositories;

namespace ShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly CategoryRepository _categoryRepository;

  public CategoriesController(CategoryRepository categoryRepository)
  {
    _categoryRepository =  categoryRepository;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
  public async Task<IActionResult> GetCategories()
  {
    var categories = await _categoryRepository.Get();

    return Ok(categories);
  }

  [HttpGet("{categoryName}")]
  [ProducesResponseType(200, Type = typeof(Product))]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  public async Task<IActionResult> GetProductsByCategory(string categoryName)
  {
    var products = await _categoryRepository.GetProductsByCategoryName(categoryName);

    if (products.Count == 0)
      return NotFound();

    return Ok(products);
  }
}
