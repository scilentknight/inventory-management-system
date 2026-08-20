using IMS.Api.DTOs.Product;
using IMS.Api.Models;
using IMS.Api.Repositories.Brands;
using IMS.Api.Repositories.Categories;
using IMS.Api.Repositories.Products;

// This is where your business logic lives.
namespace IMS.Api.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _environment = environment;
        }

        public async Task<IEnumerable<ListProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x => new ListProductDto
            {
                Id = x.Id,
                Sku = x.Sku,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                MobileImageUrl = x.MobileImageUrl,
                CategoryId = x.CategoryId,
                BrandId = x.BrandId,
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
            // Check duplicate SKU
            var existingProduct =
                await _repository.GetBySkuAsync(dto.Sku);

            if (existingProduct != null)
            {
                throw new InvalidOperationException(
                    "A product with this SKU already exists.");
            }

            // Validate category (optional)
            if (dto.CategoryId.HasValue)
            {
                var categoryExists =
                    await _categoryRepository.ExistsAsync(dto.CategoryId.Value);

                if (!categoryExists)
                {
                    throw new InvalidOperationException(
                        "The selected category does not exist.");
                }
            }

            // Validate brand (optional)
            if (dto.BrandId.HasValue)
            {
                var brandExists =
                    await _brandRepository.ExistsAsync(dto.BrandId.Value);

                if (!brandExists)
                {
                    throw new InvalidOperationException(
                        "The selected brand does not exist.");
                }
            }

            // Create entity
            var product = new Product
            {
                Sku = dto.Sku.Trim(),
                Name = dto.Name.Trim(),
                Slug = string.IsNullOrWhiteSpace(dto.Slug)
                    ? GenerateSlug(dto.Name)
                    : dto.Slug.Trim(),
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId,
                Unit = dto.Unit,
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

            // Save uploaded image
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

            // Reload product so navigation properties are available
            var createdProduct =
                await _repository.GetByIdAsync(product.Id);

            return MapToDto(createdProduct!);
        }

        public async Task<ProductDto?> UpdateAsync(
            int id,
            UpdateProductDto dto,
            int updatedBy)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            // Validate category (optional)
            if (dto.CategoryId.HasValue)
            {
                var categoryExists =
                    await _categoryRepository.ExistsAsync(dto.CategoryId.Value);

                if (!categoryExists)
                {
                    throw new InvalidOperationException(
                        "The selected category does not exist.");
                }
            }

            // Validate brand (optional)
            if (dto.BrandId.HasValue)
            {
                var brandExists =
                    await _brandRepository.ExistsAsync(dto.BrandId.Value);

                if (!brandExists)
                {
                    throw new InvalidOperationException(
                        "The selected brand does not exist.");
                }
            }

            product.Name = dto.Name.Trim();
            product.Description = dto.Description;
            product.CategoryId = dto.CategoryId;
            product.BrandId = dto.BrandId;
            product.Unit = dto.Unit;
            product.Price = dto.Price;
            product.CostPrice = dto.CostPrice;
            product.DiscountPrice = dto.DiscountPrice;
            product.StockQuantity = dto.StockQuantity;
            product.ReorderLevel = dto.ReorderLevel;
            product.DisplayOrder = dto.DisplayOrder;
            product.IsActive = dto.IsActive;
            product.UpdatedAt = DateTime.UtcNow;
            product.UpdatedBy = updatedBy;

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
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            // Name
            if (dto.Name != null)
            {
                product.Name = dto.Name.Trim();

                // Regenerate slug when name changes
                product.Slug = GenerateSlug(product.Name);
            }

            // Description
            if (dto.Description != null)
            {
                product.Description = dto.Description.Trim();
            }

            // Category (optional)
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

                product.CategoryId = dto.CategoryId.Value;
            }

            // Brand (optional)
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

                product.BrandId = dto.BrandId.Value;
            }

            // Unit
            if (dto.Unit != null)
            {
                product.Unit = dto.Unit.Trim();
            }

            // Price
            if (dto.Price.HasValue)
            {
                product.Price = dto.Price.Value;
            }

            // Cost Price
            if (dto.CostPrice.HasValue)
            {
                product.CostPrice = dto.CostPrice.Value;
            }

            // Discount Price
            if (dto.DiscountPrice.HasValue)
            {
                product.DiscountPrice = dto.DiscountPrice.Value;
            }

            // Stock Quantity
            if (dto.StockQuantity.HasValue)
            {
                product.StockQuantity = dto.StockQuantity.Value;
            }

            // Reorder Level
            if (dto.ReorderLevel.HasValue)
            {
                product.ReorderLevel = dto.ReorderLevel.Value;
            }

            // Display Order
            if (dto.DisplayOrder.HasValue)
            {
                product.DisplayOrder = dto.DisplayOrder.Value;
            }

            // Active Status
            if (dto.IsActive.HasValue)
            {
                product.IsActive = dto.IsActive.Value;
            }

            // Image
            if (dto.Image != null)
            {
                product.ImageUrl =
                    await SaveImageAsync(dto.Image);
            }

            // Mobile Image
            if (dto.MobileImage != null)
            {
                product.MobileImageUrl =
                    await SaveMobileImageAsync(dto.MobileImage);
            }

            // Audit
            product.UpdatedAt = DateTime.UtcNow;
            product.UpdatedBy = updatedBy;

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
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            // Soft delete
            product.IsDeleted = true;
            product.IsActive = false;
            product.DeletedAt = DateTime.UtcNow;
            product.DeletedBy = deletedBy;

            _repository.Update(product);

            await _repository.SaveChangesAsync();

            return true;
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                BrandId = product.BrandId,
                BrandName = product.Brand?.Name,
                Unit = product.Unit,
                Price = product.Price,
                CostPrice = product.CostPrice,
                DiscountPrice = product.DiscountPrice,
                StockQuantity = product.StockQuantity,
                ReorderLevel = product.ReorderLevel,
                ImageUrl = product.ImageUrl,
                MobileImageUrl = product.MobileImageUrl,
                DisplayOrder = product.DisplayOrder,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt
            };
        }

        private async Task<string> SaveImageAsync(
            IFormFile image)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath ?? "wwwroot",
                "uploads",
                "products");

            Directory.CreateDirectory(uploadsFolder);

            var extension =
                Path.GetExtension(image.FileName);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath =
                Path.Combine(uploadsFolder, fileName);

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
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath ?? "wwwroot",
                "uploads",
                "products",
                "mobile");

            Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(image.FileName);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await image.CopyToAsync(stream);

            return $"/uploads/products/mobile/{fileName}";
        }

        private static string GenerateSlug(string value)
        {
            return value
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "-");
        }
    }
}