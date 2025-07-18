
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Entities;
using ShopApi.Extensions;
using ShopApi.Repositories;

namespace ShopApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
  private readonly ProductRepository _productRepository;

  public ProductsController(ProductRepository productRepository)
  {
    _productRepository = productRepository;
  }

  [HttpGet]
  [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
  public async Task<IActionResult> GetProducts()
  {
    var products = await _productRepository.Get();

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

    var product = await _productRepository.GetById(productId, User.GetUserId());

    return Ok(product);
  }
}
