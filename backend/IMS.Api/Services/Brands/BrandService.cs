using IMS.Api.DTOs.Brand;
using IMS.Api.Models;
using IMS.Api.Repositories.Brands;

// This is where your business logic lives.
namespace IMS.Api.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public BrandService(
            IBrandRepository repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public async Task<IEnumerable<ListBrandDto>> GetAllAsync()
        {
            var brands = await _repository.GetAllAsync();

            return brands.Select(x => new ListBrandDto
            {
                Id = x.Id,
                BrandCode = x.BrandCode,
                Name = x.Name,
                LogoUrl = x.LogoUrl,
                MobileLogoUrl = x.MobileLogoUrl,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive
            });
        }

        public async Task<BrandDto?> GetByIdAsync(int id)
        {
            var brand = await _repository.GetByIdAsync(id);

            if (brand == null)
                return null;

            return MapToDto(brand);
        }

        public async Task<BrandDto> CreateAsync(
            CreateBrandDto dto,
            int createdBy)
        {
            // Check duplicate brand code
            var existingBrand =
                await _repository.GetByCodeAsync(dto.BrandCode);

            if (existingBrand != null)
            {
                throw new InvalidOperationException(
                    "A brand with this brand code already exists.");
            }

            // Create entity
            var brand = new Brand
            {
                BrandCode = dto.BrandCode.Trim(),
                Name = dto.Name.Trim(),
                Slug = string.IsNullOrWhiteSpace(dto.Slug)
                    ? GenerateSlug(dto.Name)
                    : dto.Slug.Trim(),
                Description = dto.Description,
                Website = dto.Website,
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            // Save uploaded logo
            if (dto.Logo != null)
            {
                brand.LogoUrl =
                    await SaveLogoAsync(dto.Logo);
            }
            if (dto.MobileLogo != null)
            {
                brand.MobileLogoUrl =
                    await SaveMobileLogoAsync(dto.MobileLogo);
            }

            await _repository.AddAsync(brand);
            await _repository.SaveChangesAsync();

            // Reload brand so any DB-generated values are available
            var createdBrand =
                await _repository.GetByIdAsync(brand.Id);

            return MapToDto(createdBrand!);
        }

        public async Task<BrandDto?> UpdateAsync(
            int id,
            UpdateBrandDto dto,
            int updatedBy)
        {
            var brand = await _repository.GetByIdAsync(id);

            if (brand == null)
                return null;

            brand.Name = dto.Name.Trim();
            brand.Description = dto.Description;
            brand.Website = dto.Website;
            brand.DisplayOrder = dto.DisplayOrder;
            brand.IsActive = dto.IsActive;
            brand.UpdatedAt = DateTime.UtcNow;
            brand.UpdatedBy = updatedBy;

            if (dto.Logo != null)
            {
                brand.LogoUrl =
                    await SaveLogoAsync(dto.Logo);
            }
            if (dto.MobileLogo != null)
            {
                brand.MobileLogoUrl =
                    await SaveMobileLogoAsync(dto.MobileLogo);
            }

            _repository.Update(brand);
            await _repository.SaveChangesAsync();

            var updatedBrand =
                await _repository.GetByIdAsync(id);

            return MapToDto(updatedBrand!);
        }

        public async Task<bool> DeleteAsync(
            int id,
            int deletedBy)
        {
            var brand = await _repository.GetByIdAsync(id);

            if (brand == null)
                return false;

            // Soft delete
            brand.IsDeleted = true;
            brand.IsActive = false;
            brand.DeletedAt = DateTime.UtcNow;
            brand.DeletedBy = deletedBy;

            _repository.Update(brand);

            await _repository.SaveChangesAsync();

            return true;
        }

        private static BrandDto MapToDto(Brand brand)
        {
            return new BrandDto
            {
                Id = brand.Id,
                BrandCode = brand.BrandCode,
                Name = brand.Name,
                Slug = brand.Slug,
                Description = brand.Description,
                Website = brand.Website,
                LogoUrl = brand.LogoUrl,
                MobileLogoUrl = brand.MobileLogoUrl,
                DisplayOrder = brand.DisplayOrder,
                IsActive = brand.IsActive,
                CreatedAt = brand.CreatedAt
            };
        }

        private async Task<string> SaveLogoAsync(
            IFormFile logo)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath ?? "wwwroot",
                "uploads",
                "brands");

            Directory.CreateDirectory(uploadsFolder);

            var extension =
                Path.GetExtension(logo.FileName);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath =
                Path.Combine(uploadsFolder, fileName);

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await logo.CopyToAsync(stream);

            return $"/uploads/brands/{fileName}";
        }

        private async Task<string> SaveMobileLogoAsync(
            IFormFile logo)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath ?? "wwwroot",
                "uploads",
                "brands",
                "mobile");

            Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(logo.FileName);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await logo.CopyToAsync(stream);

            return $"/uploads/brands/mobile/{fileName}";
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