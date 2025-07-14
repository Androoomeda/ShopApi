namespace ShopApi.Dtos;

public record class CartItemDto
(
  int Id,
  ProductDto Product,
  int SizeId,
  string SizeLabel,
  int Quantity
);
