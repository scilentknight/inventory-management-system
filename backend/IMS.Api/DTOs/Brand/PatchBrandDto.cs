namespace IMS.Api.DTOs.Brand
{
    public class PatchBrandDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Website { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }

        public IFormFile? Logo { get; set; }

        public IFormFile? MobileLogo { get; set; }
    }
}
