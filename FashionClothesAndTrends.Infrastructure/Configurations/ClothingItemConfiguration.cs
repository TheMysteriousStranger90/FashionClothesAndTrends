using FashionClothesAndTrends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionClothesAndTrends.Infrastructure.Configurations;

public class ClothingItemConfiguration : IEntityTypeConfiguration<ClothingItem>
{
    public void Configure(EntityTypeBuilder<ClothingItem> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.PictureUrl)
            .IsRequired();

        builder.Property(c => c.Gender)
            .IsRequired();

        builder.Property(c => c.Size)
            .IsRequired();

        builder.Property(c => c.Category)
            .IsRequired();

        builder.Property(c => c.Discount)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.IsInStock)
            .IsRequired();

        builder.HasOne(c => c.ClothingBrand)
            .WithMany()
            .HasForeignKey(c => c.ClothingBrandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Ratings)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Comments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
