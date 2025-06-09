namespace ShopApi.Dtos;

public record class ProductDto(
  int Id,
  string Name,
  decimal Price,
  string Color,
  string Description,
  string Image
);
