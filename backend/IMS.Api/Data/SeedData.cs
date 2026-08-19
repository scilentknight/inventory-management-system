using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS.Api.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Apply pending migrations
            await context.Database.MigrateAsync();

            // Seed Categories
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        CategoryCode = "CAT001",
                        Name = "Electronics",
                        Slug = "electronics",
                        Description = "Electronic items",
                        DisplayOrder = 1,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },
                    new Category
                    {
                        CategoryCode = "CAT002",
                        Name = "Furniture",
                        Slug = "furniture",
                        Description = "Furniture items",
                        DisplayOrder = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Brands
            if (!await context.Brands.AnyAsync())
            {
                var brands = new List<Brand>
                {
                    new Brand
                    {
                        BrandCode = "BRD001",
                        Name = "Samsung",
                        Slug = "samsung",
                        Description = "Samsung electronics and appliances",
                        DisplayOrder = 1,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },
                    new Brand
                    {
                        BrandCode = "BRD002",
                        Name = "Apple",
                        Slug = "apple",
                        Description = "Apple electronics and accessories",
                        DisplayOrder = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },
                    new Brand
                    {
                        BrandCode = "BRD003",
                        Name = "LG",
                        Slug = "lg",
                        Description = "LG electronics and home appliances",
                        DisplayOrder = 3,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },
                    new Brand
                    {
                        BrandCode = "BRD004",
                        Name = "Sony",
                        Slug = "sony",
                        Description = "Sony electronics and entertainment products",
                        DisplayOrder = 4,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    }
                };
                await context.Brands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
            }

            // Seed Admin User
            if (!await context.Users.AnyAsync())
            {
                var adminUser = new User
                {
                    Email = "scilentknight512@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "SUPER_ADMIN",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
