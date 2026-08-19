using IMS.Api.DTOs.Category;
using IMS.Api.Models;
using IMS.Api.Repositories.Categories;

// This is where your business logic lives.
namespace IMS.Api.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public CategoryService(
            ICategoryRepository repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public async Task<IEnumerable<ListCategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(x => new ListCategoryDto
            {
                Id = x.Id,
                CategoryCode = x.CategoryCode,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                MobileImageUrl = x.MobileImageUrl,
                ParentCategoryId = x.ParentCategoryId,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return MapToDto(category);
        }

        public async Task<CategoryDto> CreateAsync(
            CreateCategoryDto dto,
            int createdBy)
        {
            // Check duplicate category code
            var existingCategory =
                await _repository.GetByCodeAsync(dto.CategoryCode);

            if (existingCategory != null)
            {
                throw new InvalidOperationException(
                    "A category with this category code already exists.");
            }

            // Validate parent category
            if (dto.ParentCategoryId.HasValue)
            {
                var parentExists =
                    await _repository.ExistsAsync(dto.ParentCategoryId.Value);

                if (!parentExists)
                {
                    throw new InvalidOperationException(
                        "The selected parent category does not exist.");
                }
            }

            // Create entity
            var category = new Category
            {
                CategoryCode = dto.CategoryCode.Trim(),
                Name = dto.Name.Trim(),
                Slug = string.IsNullOrWhiteSpace(dto.Slug)
                    ? GenerateSlug(dto.Name)
                    : dto.Slug.Trim(),
                Description = dto.Description,
                ParentCategoryId = dto.ParentCategoryId,
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            // Save uploaded image
            if (dto.Image != null)
            {
                category.ImageUrl =
                    await SaveImageAsync(dto.Image);
            }
            if (dto.MobileImage != null)
            {
                category.MobileImageUrl =
                    await SaveMobileImageAsync(dto.MobileImage);
            }

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            // Reload category so navigation properties are available
            var createdCategory =
                await _repository.GetByIdAsync(category.Id);

            return MapToDto(createdCategory!);
        }

        public async Task<CategoryDto?> UpdateAsync(
            int id,
            UpdateCategoryDto dto,
            int updatedBy)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            // Validate parent category
            if (dto.ParentCategoryId.HasValue)
            {
                if (dto.ParentCategoryId.Value == id)
                {
                    throw new InvalidOperationException(
                        "A category cannot be its own parent.");
                }

                var parentExists =
                    await _repository.ExistsAsync(dto.ParentCategoryId.Value);

                if (!parentExists)
                {
                    throw new InvalidOperationException(
                        "The selected parent category does not exist.");
                }
            }

            category.Name = dto.Name.Trim();
            category.Description = dto.Description;
            category.ParentCategoryId = dto.ParentCategoryId;
            category.DisplayOrder = dto.DisplayOrder;
            category.IsActive = dto.IsActive;
            category.UpdatedAt = DateTime.UtcNow;
            category.UpdatedBy = updatedBy;

            if (dto.Image != null)
            {
                category.ImageUrl =
                    await SaveImageAsync(dto.Image);
            }

            if (dto.MobileImage != null)
            {
                category.MobileImageUrl =
                    await SaveMobileImageAsync(dto.MobileImage);
            }

            _repository.Update(category);
            await _repository.SaveChangesAsync();

            var updatedCategory =
                await _repository.GetByIdAsync(id);

            return MapToDto(updatedCategory!);
        }

        public async Task<bool> DeleteAsync(
            int id,
            int deletedBy)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return false;

            // Soft delete
            category.IsDeleted = true;
            category.IsActive = false;
            category.DeletedAt = DateTime.UtcNow;
            category.DeletedBy = deletedBy;

            _repository.Update(category);

            await _repository.SaveChangesAsync();

            return true;
        }

        private static CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                CategoryCode = category.CategoryCode,
                Name = category.Name,
                Slug = category.Slug,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName =
                    category.ParentCategory?.Name,
                ImageUrl = category.ImageUrl,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt
            };
        }

        private async Task<string> SaveImageAsync(
            IFormFile image)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath ?? "wwwroot",
                "uploads",
                "categories");

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

            return $"/uploads/categories/{fileName}";
        }

        private async Task<string> SaveMobileImageAsync(
        IFormFile image)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath ?? "wwwroot",
                "uploads",
                "categories",
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

            return $"/uploads/categories/mobile/{fileName}";
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