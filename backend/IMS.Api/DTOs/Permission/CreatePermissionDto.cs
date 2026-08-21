using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Permission
{
    public class CreatePermissionDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Module { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}