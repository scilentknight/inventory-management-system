using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Unit
{
    // Used when updating a unit
    public class UpdateUnitDto
    {
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ShortName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}