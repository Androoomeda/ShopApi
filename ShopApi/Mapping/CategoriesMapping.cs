using ShopApi.Dtos;
using ShopApi.Entities;

namespace ShopApi.Mapping;

public static class CategoriesMapping
{
  public static CategoryDto ToDto(this Category category)
  {
    return new CategoryDto
    (
      category.Id,
      category.Name,
      category.SizeType
    );
  }
}
