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
        }
    }
}



//using IMS.Api.Models;
//using Microsoft.EntityFrameworkCore;

//namespace IMS.Api.Data
//{
//    public static class SeedData
//    {
//        public static async Task SeedAsync(ApplicationDbContext context)
//        {
//            await context.Database.MigrateAsync();

//            await SeedCategories(context);
//            await SeedBrands(context);
//            await SeedProducts(context);
//        }

//        private static async Task SeedCategories(ApplicationDbContext context)
//        {
//            if (await context.Categories.AnyAsync())
//                return;

//            var categories = new List<Category>
//            {
//                new Category
//                {
//                    CategoryCode = "CAT001",
//                    Name = "Electronics",
//                    Slug = "electronics",
//                    Description = "Electronic Items",
//                    DisplayOrder = 1,
//                    IsActive = true,
//                    CreatedAt = DateTime.UtcNow,
//                    CreatedBy = 1
//                },
//                new Category
//                {
//                    CategoryCode = "CAT002",
//                    Name = "Furniture",
//                    Slug = "furniture",
//                    Description = "Furniture Items",
//                    DisplayOrder = 2,
//                    IsActive = true,
//                    CreatedAt = DateTime.UtcNow,
//                    CreatedBy = 1
//                }
//            };

//            await context.Categories.AddRangeAsync(categories);
//            await context.SaveChangesAsync();
//        }

//        private static async Task SeedBrands(ApplicationDbContext context)
//        {
//            if (await context.Brands.AnyAsync())
//                return;

//            var brands = new List<Brand>
//            {
//                new Brand
//                {
//                    BrandCode = "BR001",
//                    Name = "Samsung",
//                    Description = "Samsung Brand",
//                    IsActive = true,
//                    CreatedAt = DateTime.UtcNow,
//                    CreatedBy = 1
//                },
//                new Brand
//                {
//                    BrandCode = "BR002",
//                    Name = "Apple",
//                    Description = "Apple Brand",
//                    IsActive = true,
//                    CreatedAt = DateTime.UtcNow,
//                    CreatedBy = 1
//                }
//            };

//            await context.Brands.AddRangeAsync(brands);
//            await context.SaveChangesAsync();
//        }

//        private static async Task SeedProducts(ApplicationDbContext context)
//        {
//            if (await context.Products.AnyAsync())
//                return;

//            var electronics = await context.Categories.FirstAsync(x => x.CategoryCode == "CAT001");
//            var samsung = await context.Brands.FirstAsync(x => x.BrandCode == "BR001");

//            var products = new List<Product>
//            {
//                new Product
//                {
//                    ProductCode = "PRD001",
//                    Name = "Samsung Galaxy S25",
//                    Description = "Android Smartphone",
//                    CategoryId = electronics.Id,
//                    BrandId = samsung.Id,
//                    CostPrice = 50000,
//                    SellingPrice = 65000,
//                    StockQuantity = 100,
//                    IsActive = true,
//                    CreatedAt = DateTime.UtcNow,
//                    CreatedBy = 1
//                }
//            };

//            await context.Products.AddRangeAsync(products);
//            await context.SaveChangesAsync();
//        }
//    }
//}