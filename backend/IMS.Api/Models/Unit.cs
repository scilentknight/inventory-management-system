using System.ComponentModel.DataAnnotations;

namespace IMS.Api.Models
{
    public class Unit
    {
        public int Id { get; set; }

        public string UnitCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public string? Description { get; set; }

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
        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}