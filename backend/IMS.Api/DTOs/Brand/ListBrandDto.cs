namespace IMS.Api.DTOs.Brand
{
    // Used for listing brands efficiently
    public class ListBrandDto
    {
        public int Id { get; set; }

        public string BrandCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        public string? MobileLogoUrl { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}