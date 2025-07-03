using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Extensions;
using ShopApi.Repositories;
using ShopApi.Services;
using ShopApi.Utilities;

namespace ShopApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      var services = builder.Services;

      services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

      services.AddControllers();

      services.AddSwaggerGen();

      services.AddApiAuthentication(builder.Configuration);

      var connectionString = builder.Configuration.GetConnectionString("Shop");
      services.AddSqlServer<ShopContext>(connectionString);

      services.AddScoped<ProductRepository>();
      services.AddScoped<CategoryRepository>();
      services.AddScoped<ShopUserRepository>();
      services.AddScoped<ShopUserService>();
      services.AddScoped<JwtProvider>();

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.WithOrigins("http://127.0.0.1:5500")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
      });

      var app = builder.Build();

      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
          c.RoutePrefix = string.Empty;
        });
      }

      app.UseHttpsRedirection();
      
      app.UseStaticFiles();
      app.UseDefaultFiles();

      app.UseCors();

      app.MapControllers();

      app.UseAuthentication();
      app.UseAuthorization();

      app.Run();
    }
  }
}
