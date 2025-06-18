namespace ShopApi.Dtos;

public record class ProductDto(
  int Id,
  string Name,
  string? Description,
  string Color,
  string CategoryName,
  decimal Price,
  decimal? DiscountPrice
);
