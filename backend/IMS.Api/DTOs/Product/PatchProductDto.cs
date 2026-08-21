using System.ComponentModel.DataAnnotations;

namespace IMS.Api.DTOs.Product
{
    public class PatchProductDto
    {
        [StringLength(50)]
        public string? ProductCode { get; set; }

        [StringLength(200, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Sku { get; set; }

        [StringLength(250)]
        public string? Slug { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }


        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public int? UnitId { get; set; }


        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? CostPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? DiscountPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? StockQuantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ReorderLevel { get; set; }


        [Range(0, int.MaxValue)]
        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }


        public IFormFile? Image { get; set; }

        public IFormFile? MobileImage { get; set; }
    }
}