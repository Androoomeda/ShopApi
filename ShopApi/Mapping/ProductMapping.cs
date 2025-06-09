using ShopApi.Entities;
using ShopApi.Dtos;

namespace ShopApi.Mapping;

public static class ProductMapping
{
  public static ProductDto ToDto(this Product product)
  {
    return new ProductDto
    (
      product.Id,
      product.Name,
      product.Price,
      product.Color,
      product.Description,
      product.Image
    );
  }
}
