namespace IMS.Api.DTOs.Role
{
    public class RolePermissionDto
    {
        public int PermissionId { get; set; }

        public string PermissionName { get; set; } = string.Empty;

        public string Module { get; set; } = string.Empty;
    }
}