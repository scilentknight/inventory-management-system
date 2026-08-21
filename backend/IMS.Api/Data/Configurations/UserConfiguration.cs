using IMS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMS.Api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // ==============================
            // Primary Key
            // ==============================
            builder.HasKey(u => u.Id);

            // ==============================
            // Email
            // ==============================
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            // ==============================
            // Password Hash
            // ==============================
            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            // ==============================
            // Role
            // ==============================
            builder.Property(u => u.RoleId)
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==============================
            // Is Active
            // ==============================
            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // ==============================
            // Created At
            // ==============================
            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}