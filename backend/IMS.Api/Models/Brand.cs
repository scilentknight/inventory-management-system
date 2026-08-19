using System.ComponentModel.DataAnnotations;

namespace IMS.Api.Models
{
    public class Brand
    {
        public int Id { get; set; }

        // When a new Brand object is created, initialize BrandCode with an empty string.
        public string BrandCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string? LogoUrl { get; set; }

        public string? MobileLogoUrl { get; set; }

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
        // If you later add a Product entity that references Brand,
        // you can uncomment the line below.
        // public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}