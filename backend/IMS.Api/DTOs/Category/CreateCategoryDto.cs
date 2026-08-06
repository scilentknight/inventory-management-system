using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Category
{
    //Used when creating a category
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        [Range(0, 9999)]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public IFormFile? Image { get; set; }
    }
}