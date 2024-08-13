using FashionClothesAndTrends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionClothesAndTrends.Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(c => c.User)
            .WithMany(user => user.Comments)
            .HasForeignKey(post => post.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.ClothingItem)
            .WithMany(ci => ci.Comments)
            .HasForeignKey(c => c.ClothingItemId);
    }
}