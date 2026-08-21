using IMS.Api.DTOs.Permission;
using IMS.Api.Models;
using IMS.Api.Repositories.Permissions;

namespace IMS.Api.Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;

        public PermissionService(
            IPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ListPermissionDto>>
            GetAllAsync()
        {
            var permissions =
                await _repository.GetAllAsync();

            return permissions.Select(p =>
                new ListPermissionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt
                });
        }

        public async Task<PermissionDto?> GetByIdAsync(
            int id)
        {
            var permission =
                await _repository.GetByIdAsync(id);

            if (permission == null)
                return null;

            return MapToDto(permission);
        }

        public async Task<PermissionDto> CreateAsync(
            CreatePermissionDto dto)
        {
            var name = dto.Name.Trim();

            var existing =
                await _repository.GetByNameAsync(name);

            if (existing != null)
            {
                throw new InvalidOperationException(
                    "A permission with this name already exists.");
            }

            var permission = new Permission
            {
                Name = name,
                Description = dto.Description?.Trim(),
                Module = dto.Module.Trim(),
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(permission);

            await _repository.SaveChangesAsync();

            return MapToDto(permission);
        }

        public async Task<PermissionDto?> UpdateAsync(
            int id,
            UpdatePermissionDto dto)
        {
            var permission =
                await _repository.GetByIdAsync(id);

            if (permission == null)
                return null;

            var name = dto.Name.Trim();

            var existing =
                await _repository.GetByNameAsync(name);

            if (existing != null &&
                existing.Id != id)
            {
                throw new InvalidOperationException(
                    "A permission with this name already exists.");
            }

            permission.Name = name;
            permission.Description =
                dto.Description?.Trim();
            permission.Module =
                dto.Module.Trim();
            permission.IsActive =
                dto.IsActive;

            _repository.Update(permission);

            await _repository.SaveChangesAsync();

            return MapToDto(permission);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permission =
                await _repository.GetByIdAsync(id);

            if (permission == null)
                return false;

            _repository.Delete(permission);

            await _repository.SaveChangesAsync();

            return true;
        }

        private static PermissionDto MapToDto(
            Permission permission)
        {
            return new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description,
                Module = permission.Module,
                IsActive = permission.IsActive,
                CreatedAt = permission.CreatedAt
            };
        }
    }
}