namespace ShopApi.Endpoints;

using ShopApi.Data;
using Microsoft.EntityFrameworkCore;


public static class ProductsEndpoints
{
  public static void MapProductsEndpoints(this WebApplication app)
  {
    app.MapGet("/products", async (ShopContext dbContext) =>
                  await dbContext.Products
                  .ToListAsync());
  }
}
