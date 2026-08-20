using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Category
{
    public class PatchCategoryDto
    {
        [StringLength(150, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        [Range(0, int.MaxValue)]
        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }

        public IFormFile? Image { get; set; }

        public IFormFile? MobileImage { get; set; }
    }
}