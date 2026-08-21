namespace IMS.Api.DTOs.Role
{
    public class ListRoleDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public int UserCount { get; set; }

        public int PermissionCount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}