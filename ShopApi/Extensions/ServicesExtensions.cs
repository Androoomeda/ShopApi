using ShopApi.Repositories;
using ShopApi.Services;
using ShopApi.Utilities;

namespace ShopApi.Extensions;

public static class ServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<ProductRepository>();
    services.AddScoped<CategoryRepository>();
    services.AddScoped<FavoriteRepository>();
    services.AddScoped<CartItemRepository>();
    services.AddScoped<ShopUserRepository>();
    services.AddScoped<ShopUserService>();
    services.AddScoped<JwtProvider>();

    return services;
  }
}
