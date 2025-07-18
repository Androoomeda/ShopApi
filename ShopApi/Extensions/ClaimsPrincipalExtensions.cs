using System.Security.Claims;

namespace ShopApi.Extensions;

public static class ClaimsPrincipalExtensions
{
  public static int? GetUserId(this ClaimsPrincipal user)
  {
    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (int.TryParse(userId, out int userIdInt))
      return userIdInt;

    return null;
  }
}
