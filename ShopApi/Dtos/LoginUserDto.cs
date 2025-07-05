using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos;

public record class LoginUserDto
(
  [Required] string Email,
  [Required] string Password
);
