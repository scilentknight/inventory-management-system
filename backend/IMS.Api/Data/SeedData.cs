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

            // Seed Products
            if (!await context.Products.AnyAsync())
            {
                var electronics = await context.Categories
                    .FirstAsync(c => c.CategoryCode == "CAT001");

                var furniture = await context.Categories
                    .FirstAsync(c => c.CategoryCode == "CAT002");

                var samsung = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD001");

                var apple = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD002");

                var lg = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD003");

                var sony = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD004");

                var products = new List<Product>
    {
        // Electronics
        new Product
        {
            Sku = "PROD001",
            Name = "Galaxy Laptop",
            Slug = "galaxy-laptop",
            Description = "High-performance Samsung laptop for work, study, and everyday use.",
            CategoryId = electronics.Id,
            BrandId = samsung.Id,
            Unit = "Piece",
            Price = 85000.00m,
            CostPrice = 70000.00m,
            DiscountPrice = 80000.00m,
            StockQuantity = 25,
            ReorderLevel = 5,
            ImageUrl = "/uploads/products/laptop.jpg",
            MobileImageUrl = "/uploads/products/laptop-mobile.jpg",
            DisplayOrder = 1,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 1
        },

        new Product
        {
            Sku = "PROD002",
            Name = "iPhone",
            Slug = "iphone",
            Description = "Apple smartphone with powerful performance and advanced features.",
            CategoryId = electronics.Id,
            BrandId = apple.Id,
            Unit = "Piece",
            Price = 125000.00m,
            CostPrice = 105000.00m,
            DiscountPrice = 118000.00m,
            StockQuantity = 30,
            ReorderLevel = 5,
            ImageUrl = "/uploads/products/iphone.jpg",
            MobileImageUrl = "/uploads/products/iphone-mobile.jpg",
            DisplayOrder = 2,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 1
        },

        new Product
        {
            Sku = "PROD003",
            Name = "Smart TV",
            Slug = "smart-tv",
            Description = "LG smart television with high-quality picture and smart features.",
            CategoryId = electronics.Id,
            BrandId = lg.Id,
            Unit = "Piece",
            Price = 75000.00m,
            CostPrice = 60000.00m,
            DiscountPrice = 70000.00m,
            StockQuantity = 20,
            ReorderLevel = 4,
            ImageUrl = "/uploads/products/smart-tv.jpg",
            MobileImageUrl = "/uploads/products/smart-tv-mobile.jpg",
            DisplayOrder = 3,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 1
        },

        new Product
        {
            Sku = "PROD004",
            Name = "Wireless Headphones",
            Slug = "wireless-headphones",
            Description = "Sony wireless headphones with clear sound and comfortable design.",
            CategoryId = electronics.Id,
            BrandId = sony.Id,
            Unit = "Piece",
            Price = 15000.00m,
            CostPrice = 11000.00m,
            DiscountPrice = 13500.00m,
            StockQuantity = 40,
            ReorderLevel = 8,
            ImageUrl = "/uploads/products/headphones.jpg",
            MobileImageUrl = "/uploads/products/headphones-mobile.jpg",
            DisplayOrder = 4,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 1
        },

        // Furniture
        new Product
        {
            Sku = "PROD005",
            Name = "Office Chair",
            Slug = "office-chair",
            Description = "Comfortable ergonomic office chair suitable for long working hours.",
            CategoryId = furniture.Id,
            BrandId = samsung.Id,
            Unit = "Piece",
            Price = 35000.00m,
            CostPrice = 28000.00m,
            DiscountPrice = 32000.00m,
            StockQuantity = 15,
            ReorderLevel = 3,
            ImageUrl = "/uploads/products/office-chair.jpg",
            MobileImageUrl = "/uploads/products/office-chair-mobile.jpg",
            DisplayOrder = 5,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 1
        },

        new Product
        {
            Sku = "PROD006",
            Name = "Office Desk",
            Slug = "office-desk",
            Description = "Modern office desk with a spacious and durable working surface.",
            CategoryId = furniture.Id,
            BrandId = lg.Id,
            Unit = "Piece",
            Price = 28000.00m,
            CostPrice = 22000.00m,
            DiscountPrice = 25000.00m,
            StockQuantity = 10,
            ReorderLevel = 2,
            ImageUrl = "/uploads/products/office-desk.jpg",
            MobileImageUrl = "/uploads/products/office-desk-mobile.jpg",
            DisplayOrder = 6,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = 1
        }
    };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
