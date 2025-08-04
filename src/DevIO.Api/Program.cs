using DevIO.Api.Configurations;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Mvc;
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

            builder.Services.AddAuthorization();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddIdentityConfiguration(builder.Configuration);

            builder.Services.AddApiConfig();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
               options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.ResolveDependencies();

            var app = builder.Build();

            app.UseApiConfig(app.Environment);

            app.UseCors("Development");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
