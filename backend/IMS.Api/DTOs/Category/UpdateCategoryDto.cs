using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Category
{
    //Used when updating a category
    public class UpdateCategoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        [Range(0, 9999)]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? Image { get; set; }
    }
}