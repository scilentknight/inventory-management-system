using IMS.Api.DTOs.Role;
using IMS.Api.Models;
using IMS.Api.Repositories.Permissions;
using IMS.Api.Repositories.Roles;

namespace IMS.Api.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleService(
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<ListRoleDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles.Select(role => new ListRoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsActive = role.IsActive,
                UserCount = role.Users.Count,
                PermissionCount = role.RolePermissions.Count,
                CreatedAt = role.CreatedAt
            });
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return null;

            return MapToDto(role);
        }

        public async Task<RoleDto> CreateAsync(
            CreateRoleDto dto)
        {
            var name = dto.Name.Trim();

            var existingRole =
                await _roleRepository.GetByNameAsync(name);

            if (existingRole != null)
            {
                throw new InvalidOperationException(
                    "A role with this name already exists.");
            }

            var role = new Role
            {
                Name = name,
                Description = dto.Description?.Trim(),
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();

            var createdRole =
                await _roleRepository.GetByIdAsync(role.Id);

            return MapToDto(createdRole!);
        }

        public async Task<RoleDto?> UpdateAsync(
            int id,
            UpdateRoleDto dto)
        {
            var role =
                await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return null;

            var name = dto.Name.Trim();

            var existingRole =
                await _roleRepository.GetByNameAsync(name);

            if (existingRole != null &&
                existingRole.Id != id)
            {
                throw new InvalidOperationException(
                    "A role with this name already exists.");
            }

            role.Name = name;
            role.Description = dto.Description?.Trim();
            role.IsActive = dto.IsActive;

            _roleRepository.Update(role);

            await _roleRepository.SaveChangesAsync();

            return MapToDto(role);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role =
                await _roleRepository.GetByIdAsync(id);

            if (role == null)
                return false;

            if (role.Users.Any())
            {
                throw new InvalidOperationException(
                    "This role cannot be deleted because it is assigned to one or more users.");
            }

            _roleRepository.Delete(role);

            await _roleRepository.SaveChangesAsync();

            return true;
        }

        public async Task<RoleDto?> AssignPermissionsAsync(
            int roleId,
            AssignPermissionsDto dto)
        {
            var role =
                await _roleRepository.GetByIdAsync(roleId);

            if (role == null)
                return null;

            var permissionIds =
                dto.PermissionIds
                    .Distinct()
                    .ToList();

            var permissions =
                await _permissionRepository
                    .GetByIdsAsync(permissionIds);

            if (permissions.Count != permissionIds.Count)
            {
                throw new InvalidOperationException(
                    "One or more selected permissions do not exist.");
            }

            role.RolePermissions.Clear();

            foreach (var permission in permissions)
            {
                role.RolePermissions.Add(
                    new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permission.Id
                    });
            }

            await _roleRepository.SaveChangesAsync();

            var updatedRole =
                await _roleRepository.GetByIdAsync(roleId);

            return MapToDto(updatedRole!);
        }

        private static RoleDto MapToDto(Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsActive = role.IsActive,
                CreatedAt = role.CreatedAt,

                Permissions = role.RolePermissions
                    .Where(rp => rp.Permission != null)
                    .Select(rp => new RolePermissionDto
                    {
                        PermissionId = rp.Permission.Id,
                        PermissionName = rp.Permission.Name,
                        Module = rp.Permission.Module
                    })
                    .ToList()
            };
        }
    }
}