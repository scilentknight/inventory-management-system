namespace IMS.Api.DTOs.Product
{
    //Used for listing products efficiently
    public class ListProductDto
    {
        public int Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? MobileImageUrl { get; set; }

        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int? DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}