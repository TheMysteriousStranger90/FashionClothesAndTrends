using FashionClothesAndTrends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionClothesAndTrends.Infrastructure.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<FavoriteItem>
{
    public void Configure(EntityTypeBuilder<FavoriteItem> builder)
    {
        builder.HasOne(favorite => favorite.User)
            .WithMany(user => user.FavoriteItems)
            .HasForeignKey(favorite => favorite.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(favorite => favorite.ClothingItem)
            .WithMany(ci => ci.FavoriteItems)
            .HasForeignKey(favorite => favorite.ClothingItemId);
    }
}