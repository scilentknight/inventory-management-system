using IMS.Api.DTOs.Product;
using IMS.Api.Models;
using IMS.Api.Repositories.Brands;
using IMS.Api.Repositories.Categories;
using IMS.Api.Repositories.Products;
using IMS.Api.Repositories.Units;

namespace IMS.Api.Services.Products
{
    // This is where your business logic lives.
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IUnitRepository unitRepository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _unitRepository = unitRepository;
            _environment = environment;
        }

        public async Task<IEnumerable<ListProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x => new ListProductDto
            {
                Id = x.Id,
                ProductCode = x.ProductCode,
                Sku = x.Sku,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                MobileImageUrl = x.MobileImageUrl,
                CategoryId = x.CategoryId,
                BrandId = x.BrandId,
                UnitId = x.UnitId,
                UnitName = x.Unit?.Name,
                UnitSymbol = x.Unit?.ShortName,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive
            });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return MapToDto(product);
        }

        public async Task<ProductDto> CreateAsync(
            CreateProductDto dto,
            int createdBy)
        {
            // ==============================
            // Check duplicate Product Code
            // ==============================
            var existingProductCode =
                await _repository.GetByProductCodeAsync(
                    dto.ProductCode.Trim());

            if (existingProductCode != null)
            {
                throw new InvalidOperationException(
                    "A product with this product code already exists.");
            }


            // ==============================
            // Check duplicate SKU
            // ==============================
            var existingSku =
                await _repository.GetBySkuAsync(
                    dto.Sku.Trim());

            if (existingSku != null)
            {
                throw new InvalidOperationException(
                    "A product with this SKU already exists.");
            }


            // ==============================
            // Validate Category
            // ==============================
            if (dto.CategoryId.HasValue)
            {
                var categoryExists =
                    await _categoryRepository.ExistsAsync(
                        dto.CategoryId.Value);

                if (!categoryExists)
                {
                    throw new InvalidOperationException(
                        "The selected category does not exist.");
                }
            }


            // ==============================
            // Validate Brand
            // ==============================
            if (dto.BrandId.HasValue)
            {
                var brandExists =
                    await _brandRepository.ExistsAsync(
                        dto.BrandId.Value);

                if (!brandExists)
                {
                    throw new InvalidOperationException(
                        "The selected brand does not exist.");
                }
            }


            // ==============================
            // Validate Unit
            // ==============================
            if (dto.UnitId.HasValue)
            {
                var unitExists =
                    await _unitRepository.ExistsAsync(
                        dto.UnitId.Value);

                if (!unitExists)
                {
                    throw new InvalidOperationException(
                        "The selected unit does not exist.");
                }
            }


            // ==============================
            // Create entity
            // ==============================
            var product = new Product
            {
                ProductCode = dto.ProductCode.Trim(),

                Sku = dto.Sku.Trim(),

                Name = dto.Name.Trim(),

                Slug = string.IsNullOrWhiteSpace(dto.Slug)
                    ? GenerateSlug(dto.Name)
                    : dto.Slug.Trim(),

                Description = dto.Description,

                CategoryId = dto.CategoryId,

                BrandId = dto.BrandId,

                UnitId = dto.UnitId,

                Price = dto.Price,

                CostPrice = dto.CostPrice,

                DiscountPrice = dto.DiscountPrice,

                StockQuantity = dto.StockQuantity,

                ReorderLevel = dto.ReorderLevel,

                DisplayOrder = dto.DisplayOrder,

                IsActive = dto.IsActive,

                IsDeleted = false,

                CreatedAt = DateTime.UtcNow,

                CreatedBy = createdBy
            };


            // ==============================
            // Save Image
            // ==============================
            if (dto.Image != null)
            {
                product.ImageUrl =
                    await SaveImageAsync(dto.Image);
            }

            if (dto.MobileImage != null)
            {
                product.MobileImageUrl =
                    await SaveMobileImageAsync(dto.MobileImage);
            }


            await _repository.AddAsync(product);

            await _repository.SaveChangesAsync();


            // Reload product with navigation properties
            var createdProduct =
                await _repository.GetByIdAsync(product.Id);

            return MapToDto(createdProduct!);
        }

        public async Task<ProductDto?> UpdateAsync(
            int id,
            UpdateProductDto dto,
            int updatedBy)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
                return null;


            // ==============================
            // Check duplicate Product Code
            // ==============================
            if (!string.Equals(
                product.ProductCode,
                dto.ProductCode.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                var existingProductCode =
                    await _repository.GetByProductCodeAsync(
                        dto.ProductCode.Trim());

                if (existingProductCode != null &&
                    existingProductCode.Id != id)
                {
                    throw new InvalidOperationException(
                        "A product with this product code already exists.");
                }
            }


            // ==============================
            // Check duplicate SKU
            // ==============================
            if (!string.Equals(
                product.Sku,
                dto.Sku.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                var existingSku =
                    await _repository.GetBySkuAsync(
                        dto.Sku.Trim());

                if (existingSku != null &&
                    existingSku.Id != id)
                {
                    throw new InvalidOperationException(
                        "A product with this SKU already exists.");
                }
            }


            // ==============================
            // Validate Category
            // ==============================
            if (dto.CategoryId.HasValue)
            {
                var categoryExists =
                    await _categoryRepository.ExistsAsync(
                        dto.CategoryId.Value);

                if (!categoryExists)
                {
                    throw new InvalidOperationException(
                        "The selected category does not exist.");
                }
            }


            // ==============================
            // Validate Brand
            // ==============================
            if (dto.BrandId.HasValue)
            {
                var brandExists =
                    await _brandRepository.ExistsAsync(
                        dto.BrandId.Value);

                if (!brandExists)
                {
                    throw new InvalidOperationException(
                        "The selected brand does not exist.");
                }
            }


            // ==============================
            // Validate Unit
            // ==============================
            if (dto.UnitId.HasValue)
            {
                var unitExists =
                    await _unitRepository.ExistsAsync(
                        dto.UnitId.Value);

                if (!unitExists)
                {
                    throw new InvalidOperationException(
                        "The selected unit does not exist.");
                }
            }


            // ==============================
            // Update entity
            // ==============================
            product.ProductCode =
                dto.ProductCode.Trim();

            product.Sku =
                dto.Sku.Trim();

            product.Name =
                dto.Name.Trim();

            product.Slug =
                string.IsNullOrWhiteSpace(dto.Slug)
                    ? GenerateSlug(dto.Name)
                    : dto.Slug.Trim();

            product.Description =
                dto.Description;

            product.CategoryId =
                dto.CategoryId;

            product.BrandId =
                dto.BrandId;

            product.UnitId =
                dto.UnitId;

            product.Price =
                dto.Price;

            product.CostPrice =
                dto.CostPrice;

            product.DiscountPrice =
                dto.DiscountPrice;

            product.StockQuantity =
                dto.StockQuantity;

            product.ReorderLevel =
                dto.ReorderLevel;

            product.DisplayOrder =
                dto.DisplayOrder;

            product.IsActive =
                dto.IsActive;

            product.UpdatedAt =
                DateTime.UtcNow;

            product.UpdatedBy =
                updatedBy;


            // ==============================
            // Images
            // ==============================
            if (dto.Image != null)
            {
                product.ImageUrl =
                    await SaveImageAsync(dto.Image);
            }

            if (dto.MobileImage != null)
            {
                product.MobileImageUrl =
                    await SaveMobileImageAsync(dto.MobileImage);
            }


            _repository.Update(product);

            await _repository.SaveChangesAsync();


            var updatedProduct =
                await _repository.GetByIdAsync(id);

            return MapToDto(updatedProduct!);
        }

        public async Task<ProductDto?> PatchAsync(
            int id,
            PatchProductDto dto,
            int updatedBy)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
                return null;


            // ==============================
            // Product Code
            // ==============================
            if (dto.ProductCode != null)
            {
                var productCode =
                    dto.ProductCode.Trim();

                if (!string.Equals(
                    product.ProductCode,
                    productCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    var existingProductCode =
                        await _repository.GetByProductCodeAsync(
                            productCode);

                    if (existingProductCode != null &&
                        existingProductCode.Id != id)
                    {
                        throw new InvalidOperationException(
                            "A product with this product code already exists.");
                    }

                    product.ProductCode =
                        productCode;
                }
            }


            // ==============================
            // SKU
            // ==============================
            if (dto.Sku != null)
            {
                var sku = dto.Sku.Trim();

                if (!string.Equals(
                    product.Sku,
                    sku,
                    StringComparison.OrdinalIgnoreCase))
                {
                    var existingSku =
                        await _repository.GetBySkuAsync(sku);

                    if (existingSku != null &&
                        existingSku.Id != id)
                    {
                        throw new InvalidOperationException(
                            "A product with this SKU already exists.");
                    }

                    product.Sku = sku;
                }
            }


            // ==============================
            // Name
            // ==============================
            if (dto.Name != null)
            {
                product.Name =
                    dto.Name.Trim();

                product.Slug =
                    GenerateSlug(product.Name);
            }


            // ==============================
            // Slug
            // ==============================
            if (dto.Slug != null)
            {
                product.Slug =
                    dto.Slug.Trim();
            }


            // ==============================
            // Description
            // ==============================
            if (dto.Description != null)
            {
                product.Description =
                    dto.Description.Trim();
            }


            // ==============================
            // Category
            // ==============================
            if (dto.CategoryId.HasValue)
            {
                var categoryExists =
                    await _categoryRepository.ExistsAsync(
                        dto.CategoryId.Value);

                if (!categoryExists)
                {
                    throw new InvalidOperationException(
                        "The selected category does not exist.");
                }

                product.CategoryId =
                    dto.CategoryId.Value;
            }


            // ==============================
            // Brand
            // ==============================
            if (dto.BrandId.HasValue)
            {
                var brandExists =
                    await _brandRepository.ExistsAsync(
                        dto.BrandId.Value);

                if (!brandExists)
                {
                    throw new InvalidOperationException(
                        "The selected brand does not exist.");
                }

                product.BrandId =
                    dto.BrandId.Value;
            }


            // ==============================
            // Unit
            // ==============================
            if (dto.UnitId.HasValue)
            {
                var unitExists =
                    await _unitRepository.ExistsAsync(
                        dto.UnitId.Value);

                if (!unitExists)
                {
                    throw new InvalidOperationException(
                        "The selected unit does not exist.");
                }

                product.UnitId =
                    dto.UnitId.Value;
            }


            // ==============================
            // Price
            // ==============================
            if (dto.Price.HasValue)
            {
                product.Price =
                    dto.Price.Value;
            }


            // ==============================
            // Cost Price
            // ==============================
            if (dto.CostPrice.HasValue)
            {
                product.CostPrice =
                    dto.CostPrice.Value;
            }


            // ==============================
            // Discount Price
            // ==============================
            if (dto.DiscountPrice.HasValue)
            {
                product.DiscountPrice =
                    dto.DiscountPrice.Value;
            }


            // ==============================
            // Stock Quantity
            // ==============================
            if (dto.StockQuantity.HasValue)
            {
                product.StockQuantity =
                    dto.StockQuantity.Value;
            }


            // ==============================
            // Reorder Level
            // ==============================
            if (dto.ReorderLevel.HasValue)
            {
                product.ReorderLevel =
                    dto.ReorderLevel.Value;
            }


            // ==============================
            // Display Order
            // ==============================
            if (dto.DisplayOrder.HasValue)
            {
                product.DisplayOrder =
                    dto.DisplayOrder.Value;
            }


            // ==============================
            // Active Status
            // ==============================
            if (dto.IsActive.HasValue)
            {
                product.IsActive =
                    dto.IsActive.Value;
            }


            // ==============================
            // Images
            // ==============================
            if (dto.Image != null)
            {
                product.ImageUrl =
                    await SaveImageAsync(dto.Image);
            }

            if (dto.MobileImage != null)
            {
                product.MobileImageUrl =
                    await SaveMobileImageAsync(dto.MobileImage);
            }


            // ==============================
            // Audit
            // ==============================
            product.UpdatedAt =
                DateTime.UtcNow;

            product.UpdatedBy =
                updatedBy;


            _repository.Update(product);

            await _repository.SaveChangesAsync();


            var updatedProduct =
                await _repository.GetByIdAsync(id);

            return MapToDto(updatedProduct!);
        }

        public async Task<bool> DeleteAsync(
            int id,
            int deletedBy)
        {
            var product =
                await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            product.IsDeleted = true;

            product.IsActive = false;

            product.DeletedAt =
                DateTime.UtcNow;

            product.DeletedBy =
                deletedBy;

            _repository.Update(product);

            await _repository.SaveChangesAsync();

            return true;
        }


        // ==============================
        // Mapping
        // ==============================
        private static ProductDto MapToDto(
            Product product)
        {
            return new ProductDto
            {
                Id = product.Id,

                ProductCode =
                    product.ProductCode,

                Sku =
                    product.Sku,

                Name =
                    product.Name,

                Slug =
                    product.Slug,

                Description =
                    product.Description,


                CategoryId =
                    product.CategoryId,

                CategoryName =
                    product.Category?.Name,


                BrandId =
                    product.BrandId,

                BrandName =
                    product.Brand?.Name,


                UnitId =
                    product.UnitId,

                UnitName =
                    product.Unit?.Name,

                UnitSymbol =
                    product.Unit?.ShortName,


                Price =
                    product.Price,

                CostPrice =
                    product.CostPrice,

                DiscountPrice =
                    product.DiscountPrice,

                StockQuantity =
                    product.StockQuantity,

                ReorderLevel =
                    product.ReorderLevel,


                ImageUrl =
                    product.ImageUrl,

                MobileImageUrl =
                    product.MobileImageUrl,

                DisplayOrder =
                    product.DisplayOrder,

                IsActive =
                    product.IsActive,

                CreatedAt =
                    product.CreatedAt
            };
        }


        // ==============================
        // Image
        // ==============================
        private async Task<string> SaveImageAsync(
            IFormFile image)
        {
            var uploadsFolder =
                Path.Combine(
                    _environment.WebRootPath ?? "wwwroot",
                    "uploads",
                    "products");

            Directory.CreateDirectory(
                uploadsFolder);

            var extension =
                Path.GetExtension(image.FileName);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    fileName);

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await image.CopyToAsync(stream);

            return $"/uploads/products/{fileName}";
        }


        private async Task<string> SaveMobileImageAsync(
            IFormFile image)
        {
            var uploadsFolder =
                Path.Combine(
                    _environment.WebRootPath ?? "wwwroot",
                    "uploads",
                    "products",
                    "mobile");

            Directory.CreateDirectory(
                uploadsFolder);

            var extension =
                Path.GetExtension(image.FileName);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    fileName);

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await image.CopyToAsync(stream);

            return $"/uploads/products/mobile/{fileName}";
        }


        private static string GenerateSlug(
            string value)
        {
            return value
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "-");
        }
    }
}