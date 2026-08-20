//using System.ComponentModel.DataAnnotations;

//namespace IMS.Api.Models
//{
//    public class Product
//    {
//        public int Id { get; set; }

//        //When a new Product object is created, initialize Sku with an empty string.
//        public string Sku { get; set; } = string.Empty;

//        public string Name { get; set; } = string.Empty;

//        public string Slug { get; set; } = string.Empty;

//        public string? Description { get; set; }

//        public int CategoryId { get; set; }

//        public string? Brand { get; set; }

//        public string? Unit { get; set; }

//        public decimal Price { get; set; }

//        public decimal? CostPrice { get; set; }

//        public decimal? DiscountPrice { get; set; }

//        public int StockQuantity { get; set; }

//        public int? ReorderLevel { get; set; }

//        public string? ImageUrl { get; set; }

//        public string? MobileImageUrl { get; set; }

//        public int? DisplayOrder { get; set; }

//        public bool IsActive { get; set; } = true;

//        public bool IsDeleted { get; set; } = false;

//        public DateTime CreatedAt { get; set; }

//        public int CreatedBy { get; set; }

//        public DateTime? UpdatedAt { get; set; }

//        public int? UpdatedBy { get; set; }

//        public DateTime? DeletedAt { get; set; }

//        public int? DeletedBy { get; set; }

//        [Timestamp]
//        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

//        // Navigation Properties
//        public Category? Category { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace IMS.Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        //When a new Product object is created, initialize Sku with an empty string.
        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        public int? BrandId { get; set; }

        public string? Unit { get; set; }

        public decimal Price { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }

        public int? ReorderLevel { get; set; }

        public string? ImageUrl { get; set; }

        public string? MobileImageUrl { get; set; }

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
        public Category? Category { get; set; }

        public Brand? Brand { get; set; }
    }
}