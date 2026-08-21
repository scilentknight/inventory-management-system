using IMS.Api.DTOs.Permission;

namespace IMS.Api.Services.Permissions
{
    public interface IPermissionService
    {
        Task<IEnumerable<ListPermissionDto>> GetAllAsync();

        Task<PermissionDto?> GetByIdAsync(int id);

        Task<PermissionDto> CreateAsync(
            CreatePermissionDto dto);

        Task<PermissionDto?> UpdateAsync(
            int id,
            UpdatePermissionDto dto);

        Task<bool> DeleteAsync(int id);
    }
}