using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Api.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // ==============================
            // Table
            // ==============================
            builder.ToTable("Categories");


            // ==============================
            // Primary Key
            // ==============================
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();


            // ==============================
            // Category Code
            // ==============================
            builder.Property(c => c.CategoryCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => c.CategoryCode)
                .IsUnique();


            // ==============================
            // Name
            // ==============================
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);


            // ==============================
            // Slug
            // ==============================
            builder.Property(c => c.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(c => c.Slug)
                .IsUnique();


            // ==============================
            // Description
            // ==============================
            builder.Property(c => c.Description)
                .HasMaxLength(1000);


            // ==============================
            // Image URL
            // ==============================
            builder.Property(c => c.ImageUrl)
                .HasMaxLength(500);

            // ==============================
            // Mobile Image URL
            // ==============================
            builder.Property(c => c.MobileImageUrl)
                .HasMaxLength(500);

            // ==============================
            // Display Order
            // ==============================
            builder.Property(c => c.DisplayOrder)
                .IsRequired(false)
                .HasDefaultValue(0);


            // ==============================
            // Is Active
            // ==============================
            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);


            // ==============================
            // Soft Delete
            // ==============================
            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            // ==============================
            // Created Audit Fields
            // ==============================
            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .IsRequired();


            // ==============================
            // Updated Audit Fields
            // ==============================
            builder.Property(c => c.UpdatedAt)
                .IsRequired(false);

            builder.Property(c => c.UpdatedBy)
                .IsRequired(false);


            // ==============================
            // Deleted Audit Fields
            // ==============================
            builder.Property(c => c.DeletedAt)
                .IsRequired(false);

            builder.Property(c => c.DeletedBy)
                .IsRequired(false);


            // ==============================
            // Row Version / Concurrency
            // ==============================
            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();


            // ==============================
            // Self-Referencing Relationship
            // ==============================
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);


            // ==============================
            // Parent Category Index
            // ==============================
            builder.HasIndex(c => c.ParentCategoryId);


            // ==============================
            // Useful Indexes
            // ==============================
            builder.HasIndex(c => c.IsActive);

            builder.HasIndex(c => c.IsDeleted);

            builder.HasIndex(c => c.DisplayOrder);
        }
    }
}