using System.ComponentModel.DataAnnotations;

namespace IMS.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        //When a new Category object is created, initialize CategoryCode with an empty string.
        public string CategoryCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public string? MobileImageUrl { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int? DeletedBy { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        // Navigation Properties
        public Category? ParentCategory { get; set; }

        public ICollection<Category> Children { get; set; } = new List<Category>();

    }
}
