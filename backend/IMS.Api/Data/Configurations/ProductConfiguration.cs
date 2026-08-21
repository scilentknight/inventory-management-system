using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Api.Data.Configurations
{
    public class ProductConfiguration
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(
            EntityTypeBuilder<Product> builder)
        {
            // ==============================
            // Table
            // ==============================
            builder.ToTable("Products");


            // ==============================
            // Primary Key
            // ==============================
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();


            // ==============================
            // Product Code
            // ==============================
            builder.Property(p => p.ProductCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.ProductCode)
                .IsUnique();


            // ==============================
            // SKU
            // ==============================
            builder.Property(p => p.Sku)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.Sku)
                .IsUnique();


            // ==============================
            // Name
            // ==============================
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);


            // ==============================
            // Slug
            // ==============================
            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(p => p.Slug)
                .IsUnique();


            // ==============================
            // Description
            // ==============================
            builder.Property(p => p.Description)
                .HasMaxLength(2000);


            // ==============================
            // Pricing
            // ==============================
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CostPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DiscountPrice)
                .HasColumnType("decimal(18,2)");


            // ==============================
            // Stock
            // ==============================
            builder.Property(p => p.StockQuantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(p => p.ReorderLevel)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");


            // ==============================
            // Image URLs
            // ==============================
            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            builder.Property(p => p.MobileImageUrl)
                .HasMaxLength(500);


            // ==============================
            // Display Order
            // ==============================
            builder.Property(p => p.DisplayOrder)
                .IsRequired(false)
                .HasDefaultValue(0);


            // ==============================
            // Is Active
            // ==============================
            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);


            // ==============================
            // Soft Delete
            // ==============================
            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            // ==============================
            // Created Audit Fields
            // ==============================
            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .IsRequired();


            // ==============================
            // Updated Audit Fields
            // ==============================
            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder.Property(p => p.UpdatedBy)
                .IsRequired(false);


            // ==============================
            // Deleted Audit Fields
            // ==============================
            builder.Property(p => p.DeletedAt)
                .IsRequired(false);

            builder.Property(p => p.DeletedBy)
                .IsRequired(false);


            // ==============================
            // Row Version / Concurrency
            // ==============================
            builder.Property(p => p.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();


            // ==============================
            // Category Relationship
            // ==============================
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CategoryId);


            // ==============================
            // Brand Relationship
            // ==============================
            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.BrandId);


            // ==============================
            // Unit Relationship
            // ==============================
            builder.HasOne(p => p.Unit)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UnitId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.UnitId);


            // ==============================
            // Useful Indexes
            // ==============================
            builder.HasIndex(p => p.IsActive);

            builder.HasIndex(p => p.IsDeleted);

            builder.HasIndex(p => p.DisplayOrder);
        }
    }
}