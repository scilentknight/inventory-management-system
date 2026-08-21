namespace IMS.Api.DTOs.Product
{
    // Used when returning a single product's details
    public class ProductDto
    {
        public int Id { get; set; }

        public string ProductCode { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }


        public int? CategoryId { get; set; }

        public string? CategoryName { get; set; }


        public int? BrandId { get; set; }

        public string? BrandName { get; set; }


        public int? UnitId { get; set; }

        public string? UnitName { get; set; }

        public string? UnitSymbol { get; set; }


        public decimal Price { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public decimal StockQuantity { get; set; }

        public decimal? ReorderLevel { get; set; }


        public string? ImageUrl { get; set; }

        public string? MobileImageUrl { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}