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
      product.Color,
      product.Price,
      product.DiscountPrice,
      product.ProductImages.First().ImagePath
    );
  }

  public static ProductDetailsDto ToDetailsDto(this Product product,
    List<SizeDto> sizes, bool isFavorite, bool isInCart)
  {
    return new ProductDetailsDto
    (
      product.Id,
      product.Name,
      product.Description,
      product.Color,
      product.CategoryId,
      product.Category.Name,
      product.Price,
      product.DiscountPrice,
      isFavorite,
      isInCart,
      product.ProductImages.Select(img => img.ImagePath).ToList(),
      sizes
    );
  }
}

