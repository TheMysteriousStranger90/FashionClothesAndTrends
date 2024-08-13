using FashionClothesAndTrends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionClothesAndTrends.Infrastructure.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(w => w.Items)
            .WithOne(wi => wi.Wishlist)
            .HasForeignKey(wi => wi.WishlistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.User)
            .WithMany(u => u.Wishlists)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
