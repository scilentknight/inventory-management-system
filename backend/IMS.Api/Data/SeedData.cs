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

            // =========================================================
            // Seed Roles
            // =========================================================

            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role>
    {
        new Role
        {
            Name = "SUPER_ADMIN",
            Description = "Full system access",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        },
        new Role
        {
            Name = "ADMIN",
            Description = "Administrative access",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        },
        new Role
        {
            Name = "STAFF",
            Description = "Standard staff access",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        }
    };

                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }


            // =========================================================
            // Seed Permissions
            // =========================================================

            if (!await context.Permissions.AnyAsync())
            {
                var permissions = new List<Permission>
    {
        new Permission { Name = "PRODUCT_VIEW", Module = "Product", IsActive = true, CreatedAt = DateTime.UtcNow },
        new Permission { Name = "PRODUCT_CREATE", Module = "Product", IsActive = true, CreatedAt = DateTime.UtcNow },
        new Permission { Name = "PRODUCT_EDIT", Module = "Product", IsActive = true, CreatedAt = DateTime.UtcNow },
        new Permission { Name = "PRODUCT_DELETE", Module = "Product", IsActive = true, CreatedAt = DateTime.UtcNow },
        new Permission { Name = "USER_MANAGE", Module = "User", IsActive = true, CreatedAt = DateTime.UtcNow }
    };

                await context.Permissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }


            // =========================================================
            // Seed RolePermissions (SUPER_ADMIN gets everything)
            // =========================================================

            if (!await context.RolePermissions.AnyAsync())
            {
                var superAdminRole = await context.Roles
                    .FirstAsync(r => r.Name == "SUPER_ADMIN");

                var allPermissions = await context.Permissions.ToListAsync();

                var rolePermissions = allPermissions
                    .Select(p => new RolePermission
                    {
                        RoleId = superAdminRole.Id,
                        PermissionId = p.Id
                    })
                    .ToList();

                await context.RolePermissions.AddRangeAsync(rolePermissions);
                await context.SaveChangesAsync();
            }


            // =========================================================
            // Seed Admin User
            // =========================================================

            if (!await context.Users.AnyAsync())
            {
                var superAdminRole = await context.Roles
                    .FirstAsync(r => r.Name == "SUPER_ADMIN");

                var adminUser = new User
                {
                    Email = "scilentknight512@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    RoleId = superAdminRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }


            // =========================================================
            // Seed Categories
            // =========================================================

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


            // =========================================================
            // Seed Brands
            // =========================================================

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


            // =========================================================
            // Seed Units
            // =========================================================

            if (!await context.Units.AnyAsync())
            {
                var units = new List<Unit>
                {
                    new Unit
                    {
                        UnitCode = "UNIT001",
                        Name = "Piece",
                        ShortName = "Pcs",
                        Description = "Individual items or pieces",
                        DisplayOrder = 1,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },

                    new Unit
                    {
                        UnitCode = "UNIT002",
                        Name = "Kilogram",
                        ShortName = "Kg",
                        Description = "Weight measured in kilograms",
                        DisplayOrder = 2,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },

                    new Unit
                    {
                        UnitCode = "UNIT003",
                        Name = "Gram",
                        ShortName = "g",
                        Description = "Weight measured in grams",
                        DisplayOrder = 3,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },

                    new Unit
                    {
                        UnitCode = "UNIT004",
                        Name = "Liter",
                        ShortName = "Ltr",
                        Description = "Volume measured in liters",
                        DisplayOrder = 4,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },

                    new Unit
                    {
                        UnitCode = "UNIT005",
                        Name = "Meter",
                        ShortName = "M",
                        Description = "Length measured in meters",
                        DisplayOrder = 5,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },

                    new Unit
                    {
                        UnitCode = "UNIT006",
                        Name = "Box",
                        ShortName = "Box",
                        Description = "Products sold by box",
                        DisplayOrder = 6,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    }
                };

                await context.Units.AddRangeAsync(units);
                await context.SaveChangesAsync();
            }


            // =========================================================
            // Seed Products
            // =========================================================

            if (!await context.Products.AnyAsync())
            {
                // -----------------------------------------------------
                // Get Categories
                // -----------------------------------------------------

                var electronics = await context.Categories
                    .FirstAsync(c => c.CategoryCode == "CAT001");

                var furniture = await context.Categories
                    .FirstAsync(c => c.CategoryCode == "CAT002");


                // -----------------------------------------------------
                // Get Brands
                // -----------------------------------------------------

                var samsung = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD001");

                var apple = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD002");

                var lg = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD003");

                var sony = await context.Brands
                    .FirstAsync(b => b.BrandCode == "BRD004");


                // -----------------------------------------------------
                // Get Units
                // -----------------------------------------------------

                var piece = await context.Units
                    .FirstAsync(u => u.UnitCode == "UNIT001");

                var kilogram = await context.Units
                    .FirstAsync(u => u.UnitCode == "UNIT002");

                var gram = await context.Units
                    .FirstAsync(u => u.UnitCode == "UNIT003");

                var liter = await context.Units
                    .FirstAsync(u => u.UnitCode == "UNIT004");

                var meter = await context.Units
                    .FirstAsync(u => u.UnitCode == "UNIT005");

                var box = await context.Units
                    .FirstAsync(u => u.UnitCode == "UNIT006");


                // -----------------------------------------------------
                // Create Products
                // -----------------------------------------------------

                var products = new List<Product>
                {
                    // =================================================
                    // Electronics
                    // =================================================

                    new Product
                    {
                        ProductCode = "PROD001",
                        Sku = "GALAXY-LAPTOP",
                        Name = "Galaxy Laptop",
                        Slug = "galaxy-laptop",
                        Description =
                            "High-performance Samsung laptop for work, study, and everyday use.",

                        CategoryId = electronics.Id,
                        BrandId = samsung.Id,
                        UnitId = piece.Id,

                        Price = 85000.00m,
                        CostPrice = 70000.00m,
                        DiscountPrice = 80000.00m,

                        StockQuantity = 25,
                        ReorderLevel = 5,

                        ImageUrl = "/uploads/products/laptop.jpg",
                        MobileImageUrl =
                            "/uploads/products/mobile/laptop-mobile.jpg",

                        DisplayOrder = 1,

                        IsActive = true,
                        IsDeleted = false,

                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },


                    new Product
                    {
                        ProductCode = "PROD002",
                        Sku = "IPHONE",
                        Name = "iPhone",
                        Slug = "iphone",
                        Description =
                            "Apple smartphone with powerful performance and advanced features.",

                        CategoryId = electronics.Id,
                        BrandId = apple.Id,
                        UnitId = piece.Id,

                        Price = 125000.00m,
                        CostPrice = 105000.00m,
                        DiscountPrice = 118000.00m,

                        StockQuantity = 30,
                        ReorderLevel = 5,

                        ImageUrl = "/uploads/products/iphone.jpg",
                        MobileImageUrl =
                            "/uploads/products/mobile/iphone-mobile.jpg",

                        DisplayOrder = 2,

                        IsActive = true,
                        IsDeleted = false,

                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },


                    new Product
                    {
                        ProductCode = "PROD003",
                        Sku = "SMART-TV",
                        Name = "Smart TV",
                        Slug = "smart-tv",
                        Description =
                            "LG smart television with high-quality picture and smart features.",

                        CategoryId = electronics.Id,
                        BrandId = lg.Id,
                        UnitId = piece.Id,

                        Price = 75000.00m,
                        CostPrice = 60000.00m,
                        DiscountPrice = 70000.00m,

                        StockQuantity = 20,
                        ReorderLevel = 4,

                        ImageUrl = "/uploads/products/smart-tv.jpg",
                        MobileImageUrl =
                            "/uploads/products/mobile/smart-tv-mobile.jpg",

                        DisplayOrder = 3,

                        IsActive = true,
                        IsDeleted = false,

                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },


                    new Product
                    {
                        ProductCode = "PROD004",
                        Sku = "WIRELESS-HEADPHONES",
                        Name = "Wireless Headphones",
                        Slug = "wireless-headphones",
                        Description =
                            "Sony wireless headphones with clear sound and comfortable design.",

                        CategoryId = electronics.Id,
                        BrandId = sony.Id,
                        UnitId = piece.Id,

                        Price = 15000.00m,
                        CostPrice = 11000.00m,
                        DiscountPrice = 13500.00m,

                        StockQuantity = 40,
                        ReorderLevel = 8,

                        ImageUrl = "/uploads/products/headphones.jpg",
                        MobileImageUrl =
                            "/uploads/products/mobile/headphones-mobile.jpg",

                        DisplayOrder = 4,

                        IsActive = true,
                        IsDeleted = false,

                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },


                    // =================================================
                    // Furniture
                    // =================================================

                    new Product
                    {
                        ProductCode = "PROD005",
                        Sku = "OFFICE-CHAIR",
                        Name = "Office Chair",
                        Slug = "office-chair",
                        Description =
                            "Comfortable ergonomic office chair suitable for long working hours.",

                        CategoryId = furniture.Id,
                        BrandId = samsung.Id,
                        UnitId = piece.Id,

                        Price = 35000.00m,
                        CostPrice = 28000.00m,
                        DiscountPrice = 32000.00m,

                        StockQuantity = 15,
                        ReorderLevel = 3,

                        ImageUrl = "/uploads/products/office-chair.jpg",
                        MobileImageUrl =
                            "/uploads/products/mobile/office-chair-mobile.jpg",

                        DisplayOrder = 5,

                        IsActive = true,
                        IsDeleted = false,

                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = 1
                    },


                    new Product
                    {
                        ProductCode = "PROD006",
                        Sku = "OFFICE-DESK",
                        Name = "Office Desk",
                        Slug = "office-desk",
                        Description =
                            "Modern office desk with a spacious and durable working surface.",

                        CategoryId = furniture.Id,
                        BrandId = lg.Id,
                        UnitId = piece.Id,

                        Price = 28000.00m,
                        CostPrice = 22000.00m,
                        DiscountPrice = 25000.00m,

                        StockQuantity = 10,
                        ReorderLevel = 2,

                        ImageUrl = "/uploads/products/office-desk.jpg",
                        MobileImageUrl =
                            "/uploads/products/mobile/office-desk-mobile.jpg",

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