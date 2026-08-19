namespace IMS.Api.DTOs.Brand
{
    public class BrandDto
    {
        // Used when returning a single brand's details
        public int Id { get; set; }

        public string BrandCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string? LogoUrl { get; set; }

        public string? MobileLogoUrl { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}