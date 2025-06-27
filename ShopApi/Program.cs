using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Data;
using ShopApi.Entities;
using ShopApi.Repositories;
using ShopApi.Services;
using ShopApi.Utilities;
using System.Text;

namespace ShopApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      var services = builder.Services;

      var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions));
      services.Configure<JwtOptions>(jwtOptions);

      services.AddControllers();

      services.AddSwaggerGen();

      var connectionString = builder.Configuration.GetConnectionString("Shop");
      services.AddSqlServer<ShopContext>(connectionString);

      services.AddScoped<ShopUserRepository>();
      services.AddScoped<ShopUserService>();
      services.AddScoped<JwtProvider>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        options.TokenValidationParameters = new()
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
        };
        options.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            context.Token = context.Request.Cookies["tasty-cookies"];

            return Task.CompletedTask;
          }
        };
      });

      services.AddAuthorization();

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.WithOrigins("http://127.0.0.1:5500")
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

      // app.UseCookiePolicy(new CookiePolicyOptions
      // {
      //   MinimumSameSitePolicy = SameSiteMode.Strict,
      //   HttpOnly = HttpOnlyPolicy.Always,
      //   Secure = CookieSecurePolicy.Always
      // });

      app.MapControllers();

      app.UseCors();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseStaticFiles();
      app.UseDefaultFiles();


      app.Run();
    }
  }
}
