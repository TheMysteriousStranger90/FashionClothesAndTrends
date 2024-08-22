using FashionClothesAndTrends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionClothesAndTrends.Infrastructure.Configurations;

public class ClothingItemPhotoConfiguration : IEntityTypeConfiguration<ClothingItemPhoto>
{
    public void Configure(EntityTypeBuilder<ClothingItemPhoto> builder)
    {
        builder.HasOne(x => x.ClothingItem).WithMany(x => x.ClothingItemPhotos)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}