//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Http;

//namespace IMS.Api.DTOs.Category
//{
//    public class CategoryCreateDto
//    {
//        [Required]
//        [StringLength(50)]
//        public string CategoryCode { get; set; } = string.Empty;

//        [Required]
//        [StringLength(150, MinimumLength = 2)]
//        public string Name { get; set; } = string.Empty;

//        [StringLength(200)]
//        public string Slug { get; set; } = string.Empty;

//        [StringLength(1000)]
//        public string? Description { get; set; }

//        public int? ParentCategoryId { get; set; }

//        public IFormFile? Image { get; set; }

//        [Range(0, int.MaxValue)]
//        public int DisplayOrder { get; set; }

//        public bool IsActive { get; set; } = true;
//    }

//    public class UpdateCategoryDto
//    {
//        [Required]
//        public int Id { get; set; }

//        [Required]
//        [StringLength(150)]
//        public string Name { get; set; } = string.Empty;

//        [StringLength(500)]
//        public string? Description { get; set; }

//        public int? ParentCategoryId { get; set; }

//        [Range(0, 9999)]
//        public int DisplayOrder { get; set; }

//        public bool IsActive { get; set; }

//        public IFormFile? Image { get; set; }
//    }

//    public class ListCategoryDto
//    {
//        public int Id { get; set; }

//        public string CategoryCode { get; set; } = string.Empty;

//        public string Name { get; set; } = string.Empty;

//        public string? ImageUrl { get; set; }

//        public int DisplayOrder { get; set; }

//        public bool IsActive { get; set; }
//    }

//    public class CategoryDto
//    {
//        public int Id { get; set; }

//        public string CategoryCode { get; set; } = string.Empty;

//        public string Name { get; set; } = string.Empty;

//        public string Slug { get; set; } = string.Empty;

//        public string? Description { get; set; }

//        public int? ParentCategoryId { get; set; }

//        public string? ParentCategoryName { get; set; }

//        public string? ImageUrl { get; set; }

//        public string? MobileImageUrl { get; set; }

//        public int DisplayOrder { get; set; }

//        public bool IsActive { get; set; }

//        public DateTime CreatedAt { get; set; }
//    }
//}