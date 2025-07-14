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
  decimal? DiscountPrice,
  bool IsFavorite,
  bool IsInCart,
  ICollection<string> ImagesPath,
  ICollection<SizeDto> Sizes
);
