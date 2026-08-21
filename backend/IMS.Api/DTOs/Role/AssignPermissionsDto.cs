using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Role
{
    public class AssignPermissionsDto
    {
        [Required]
        public List<int> PermissionIds { get; set; } = new();
    }
}