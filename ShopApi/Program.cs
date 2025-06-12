using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Endpoints;

namespace ShopApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Shop");
            builder.Services.AddSqlServer<ShopContext>(connectionString);
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.MapProductsEndpoints();

            app.UseCors();

            app.Run();
        }
    }
}
