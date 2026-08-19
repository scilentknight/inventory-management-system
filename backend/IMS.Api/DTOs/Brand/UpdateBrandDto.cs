using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Brand
{
    // Used when updating a brand
    public class UpdateBrandDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(300)]
        public string? Website { get; set; }

        [Range(0, 9999)]
        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? Logo { get; set; }
        public IFormFile? MobileLogo { get; set; }
    }
}