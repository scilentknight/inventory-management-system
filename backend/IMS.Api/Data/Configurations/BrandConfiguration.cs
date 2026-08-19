using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Api.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            // ==============================
            // Table
            // ==============================
            builder.ToTable("Brands");


            // ==============================
            // Primary Key
            // ==============================
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();


            // ==============================
            // Brand Code
            // ==============================
            builder.Property(b => b.BrandCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(b => b.BrandCode)
                .IsUnique();


            // ==============================
            // Name
            // ==============================
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(150);


            // ==============================
            // Slug
            // ==============================
            builder.Property(b => b.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(b => b.Slug)
                .IsUnique();


            // ==============================
            // Description
            // ==============================
            builder.Property(b => b.Description)
                .HasMaxLength(1000);


            // ==============================
            // Website
            // ==============================
            builder.Property(b => b.Website)
                .HasMaxLength(300);


            // ==============================
            // Logo URL
            // ==============================
            builder.Property(b => b.LogoUrl)
                .HasMaxLength(500);

            // ==============================
            // Mobile Logo URL
            // ==============================
            builder.Property(b => b.MobileLogoUrl)
                .HasMaxLength(500);


            // ==============================
            // Display Order
            // ==============================
            builder.Property(b => b.DisplayOrder)
                .IsRequired(false)
                .HasDefaultValue(0);


            // ==============================
            // Is Active
            // ==============================
            builder.Property(b => b.IsActive)
                .IsRequired()
                .HasDefaultValue(true);


            // ==============================
            // Soft Delete
            // ==============================
            builder.Property(b => b.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            // ==============================
            // Created Audit Fields
            // ==============================
            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(b => b.CreatedBy)
                .IsRequired();


            // ==============================
            // Updated Audit Fields
            // ==============================
            builder.Property(b => b.UpdatedAt)
                .IsRequired(false);

            builder.Property(b => b.UpdatedBy)
                .IsRequired(false);


            // ==============================
            // Deleted Audit Fields
            // ==============================
            builder.Property(b => b.DeletedAt)
                .IsRequired(false);

            builder.Property(b => b.DeletedBy)
                .IsRequired(false);


            // ==============================
            // Row Version / Concurrency
            // ==============================
            builder.Property(b => b.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();


            // ==============================
            // Useful Indexes
            // ==============================
            builder.HasIndex(b => b.IsActive);

            builder.HasIndex(b => b.IsDeleted);

            builder.HasIndex(b => b.DisplayOrder);
        }
    }
}