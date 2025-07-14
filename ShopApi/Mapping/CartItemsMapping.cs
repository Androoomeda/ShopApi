using ShopApi.Dtos;
using ShopApi.Entities;

namespace ShopApi.Mapping;

public static class CartItemsMapping
{
  public static CartItemDto ToDto(this CartItem cartItem)
  {
    return new CartItemDto(
      cartItem.Id,
      cartItem.Product.ToDto(),
      cartItem.SizeId,
      cartItem.Size.Label,
      cartItem.Quantity
    );
  }
}
