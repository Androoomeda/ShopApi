using ShopApi.Dtos;
using ShopApi.Entities;

namespace ShopApi.Mapping;

public static class SizesMapping
{
  public static SizeDto ToDto(this Size size)
  {
    return new SizeDto
    (
      size.Id,
      size.Label
    );
  }
}
