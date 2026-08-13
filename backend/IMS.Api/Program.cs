using IMS.Api.Data;
using IMS.Api.Repositories;
using IMS.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowNextJs",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Register ApplicationDbContext with Dependency Injection and configure SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository registrations
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Service registrations
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            var app = builder.Build();

            // Uncomment this block after the initial migration and database creation
            // to seed the database with default data.
            // Seed database
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await SeedData.SeedAsync(context);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowNextJs");

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}