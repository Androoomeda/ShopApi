namespace ShopApi.Dtos;

public record class UserInfoDto
(
  string Username,
  string Email,
  int FavoritesCount,
  int CartItemsCount
);
