using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos;

public record class NewCartItem
(
  [Required] int ProductId,
  [Required] int SizeId
);