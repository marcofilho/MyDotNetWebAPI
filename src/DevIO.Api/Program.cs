using DevIO.Api.Configurations;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                    .SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();

            builder.Services.AddDbContext<DevIODbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddApiConfig();

            builder.Services.ResolveDependencies();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseApiConfig(app.Environment);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
