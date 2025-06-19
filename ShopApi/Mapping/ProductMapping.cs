using ShopApi.Dtos;
using ShopApi.Entities;

namespace ShopApi.Mapping;

public static class ProductMapping
{
  public static ProductDto ToDto(this Product product)
  {
    return new ProductDto
    (
      product.Id,
      product.Name,
      product.Description,
      product.Color,
      product.Category.Name,
      product.Price,
      product.DiscountPrice
    );
  }

  public static ProductDto ToDetailsDto(this Product product)
  {
    return new ProductDto
    (
      product.Id,
      product.Name,
      product.Description,
      product.Color,
      product.Category.Name,
      product.Price,
      product.DiscountPrice
    );
  }
}

