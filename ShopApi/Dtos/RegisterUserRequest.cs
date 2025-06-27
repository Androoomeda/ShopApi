using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dtos;

public record class RegisterUserRequest
(
  [Required] string Username,
  [Required] string Email,
  [Required] string Password
);
