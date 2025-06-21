namespace ShopApi.Dtos;

public record class ProductDto
(
  int Id,
  string Name,
  string Color,
  decimal Price,
  decimal? DiscountPrice,
  string ImagePath
);
