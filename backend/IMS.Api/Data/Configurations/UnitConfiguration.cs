using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Api.Data.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            // ==============================
            // Table
            // ==============================
            builder.ToTable("Units");


            // ==============================
            // Primary Key
            // ==============================
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();


            // ==============================
            // Unit Code
            // ==============================
            builder.Property(u => u.UnitCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(u => u.UnitCode)
                .IsUnique();


            // ==============================
            // Name
            // ==============================
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Name)
                .IsUnique();


            // ==============================
            // Short Name
            // ==============================
            builder.Property(u => u.ShortName)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(u => u.ShortName)
                .IsUnique();


            // ==============================
            // Description
            // ==============================
            builder.Property(u => u.Description)
                .HasMaxLength(500);


            // ==============================
            // Display Order
            // ==============================
            builder.Property(u => u.DisplayOrder)
                .IsRequired(false)
                .HasDefaultValue(0);


            // ==============================
            // Is Active
            // ==============================
            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);


            // ==============================
            // Soft Delete
            // ==============================
            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);


            // ==============================
            // Created Audit Fields
            // ==============================
            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.CreatedBy)
                .IsRequired();


            // ==============================
            // Updated Audit Fields
            // ==============================
            builder.Property(u => u.UpdatedAt)
                .IsRequired(false);

            builder.Property(u => u.UpdatedBy)
                .IsRequired(false);


            // ==============================
            // Deleted Audit Fields
            // ==============================
            builder.Property(u => u.DeletedAt)
                .IsRequired(false);

            builder.Property(u => u.DeletedBy)
                .IsRequired(false);


            // ==============================
            // Row Version
            // ==============================
            builder.Property(u => u.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}