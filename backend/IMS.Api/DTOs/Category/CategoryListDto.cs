using System.Reflection;

namespace IMS.Api.DTOs.Category
{
    //Used for listing categories efficiently
    public class CategoryListDto
    {
        public int Id { get; set; }

        public string CategoryCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}