using IMS.Api.DTOs.Role;

namespace IMS.Api.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<ListRoleDto>> GetAllAsync();

        Task<RoleDto?> GetByIdAsync(int id);

        Task<RoleDto> CreateAsync(CreateRoleDto dto);

        Task<RoleDto?> UpdateAsync(
            int id,
            UpdateRoleDto dto);

        Task<bool> DeleteAsync(int id);

        Task<RoleDto?> AssignPermissionsAsync(
            int roleId,
            AssignPermissionsDto dto);
    }
}