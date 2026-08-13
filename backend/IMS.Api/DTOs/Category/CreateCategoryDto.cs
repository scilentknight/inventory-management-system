//using System.ComponentModel.DataAnnotations;

//namespace IMS.Api.DTOs.Category
//{
//    //Used when creating a category
//    public class CreateCategoryDto
//    {
//        [Required]
//        [StringLength(150)]
//        public string Name { get; set; } = string.Empty;

//        [StringLength(500)]
//        public string? Description { get; set; }

//        public int? ParentCategoryId { get; set; }

//        [Range(0, 9999)]
//        public int DisplayOrder { get; set; }

//        public bool IsActive { get; set; } = true;

//        public IFormFile? Image { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IMS.Api.DTOs.Category
{
    //Used when creating a category
    public class CategoryCreateDto
    {
        [Required]
        [StringLength(50)]
        public string CategoryCode { get; set; } = string.Empty;

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Slug { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public IFormFile? Image { get; set; }

        [Range(0, int.MaxValue)]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}