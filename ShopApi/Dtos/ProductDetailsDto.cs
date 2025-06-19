namespace ShopApi.Dtos;

public record class ProductDetailsDto
(
  int Id,
  string Name,
  string? Description,
  string Color,
  int CategoryId,
  string CategoryName,
  decimal Price,
  decimal? DiscountPrice
);
