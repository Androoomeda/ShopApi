namespace ShopApi.Dtos;

public record class ProductDto(
  int Id,
  string Name,
  string? Description,
  string Color,
  decimal Price,
  decimal DiscountPrice
);
