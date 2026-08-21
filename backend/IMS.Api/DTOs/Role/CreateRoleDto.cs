using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Role
{
    public class CreateRoleDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}