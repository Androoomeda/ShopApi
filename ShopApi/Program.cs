using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Shop");
builder.Services.AddSqlServer<ShopContext>(connectionString);

var app = builder.Build();

app.MapGet("/products", async (ShopContext dbContext) =>
    await dbContext.Products
    .ToListAsync());

app.Run();
