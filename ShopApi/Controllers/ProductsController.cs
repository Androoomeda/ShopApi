using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Mapping;
using ShopApi.Data;
using ShopApi.Entities;
using Microsoft.AspNetCore.Authorization;
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

    var product = await _productRepository.GetById(productId);

    return Ok(product);
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
