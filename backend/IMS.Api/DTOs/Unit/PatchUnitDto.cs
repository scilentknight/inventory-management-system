using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Unit
{
    // Used when partially updating a unit
    public class PatchUnitDto
    {
        [StringLength(50)]
        public string? UnitCode { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(20)]
        public string? ShortName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }
    }
}