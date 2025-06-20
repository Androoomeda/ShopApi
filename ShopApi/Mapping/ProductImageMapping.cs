using ShopApi.Dtos;
using ShopApi.Entities;

namespace ShopApi.Mapping;

public static class ProductImageMapping
{
  public static ProductImageDto ToDto(this ProductImage productImage)
  {
    return new ProductImageDto
    (
      productImage.Id,
      productImage.ImagePath
    );
  }
}
