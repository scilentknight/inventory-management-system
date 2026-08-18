namespace IMS.Api.DTOs.Category
{
    public class CategoryDto
    {
        //Used when returning a single category's details
        public int Id { get; set; }

        public string CategoryCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public string? ParentCategoryName { get; set; }

        public string? ImageUrl { get; set; }
        public string? MobileImageUrl { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}