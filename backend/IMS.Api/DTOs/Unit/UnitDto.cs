namespace IMS.Api.DTOs.Unit
{
    // Used when returning a single unit's details
    public class UnitDto
    {
        public int Id { get; set; }

        public string UnitCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}