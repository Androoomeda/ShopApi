namespace ShopApi.Endpoints;

using ShopApi.Data;
using Microsoft.EntityFrameworkCore;
using ShopApi.Dtos;
using System.IO.Pipes;
using ShopApi.Mapping;

public static class ProductsEndpoints
{
  public static void MapProductsEndpoints(this WebApplication app)
  {
    app.MapGet("/products", async (ShopContext dbContext) =>
                  await dbContext.Products
                  .Include(p => p.Category)
                  .Select(p => p.ToDto())
                  .ToListAsync());
  }
}
