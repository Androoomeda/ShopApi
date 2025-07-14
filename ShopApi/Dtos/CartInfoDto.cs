namespace ShopApi.Dtos;

public record class CartInfoDto
(
  List<CartItemDto> CartItems,
  int TotalQuantity,
  decimal TotalOriginalPrice,
  decimal? TotalDiscount,
  decimal? TotalPrice
);
