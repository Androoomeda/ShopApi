using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos;

public record class LoginUserRequest
(
  [Required] string Email,
  [Required] string Password
);
