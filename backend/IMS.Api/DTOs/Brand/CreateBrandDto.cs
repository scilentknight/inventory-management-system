using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Brand
{
    // Used when creating a brand
    public class CreateBrandDto
    {
        [Required]
        [StringLength(50)]
        public string BrandCode { get; set; } = string.Empty;

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Slug { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(300)]
        public string? Website { get; set; }

        public IFormFile? Logo { get; set; }
        public IFormFile? MobileLogo { get; set; }

        [Range(0, int.MaxValue)]
        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}