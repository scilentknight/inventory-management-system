using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Product
{
    // Used when creating a product
    public class CreateProductDto
    {
        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Sku { get; set; } = string.Empty;

        [StringLength(250)]
        public string Slug { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }


        // Optional - selected from category dropdown
        public int? CategoryId { get; set; }

        // Optional - selected from brand dropdown
        public int? BrandId { get; set; }

        // Optional - selected from unit dropdown
        public int? UnitId { get; set; }


        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? CostPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? DiscountPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal StockQuantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ReorderLevel { get; set; }


        public IFormFile? Image { get; set; }

        public IFormFile? MobileImage { get; set; }


        [Range(0, int.MaxValue)]
        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}