using FashionClothesAndTrends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionClothesAndTrends.Infrastructure.Configurations;

public class LikeDislikeConfiguration : IEntityTypeConfiguration<LikeDislike>
{
    public void Configure(EntityTypeBuilder<LikeDislike> builder)
    {
        builder.HasOne(likeDislike => likeDislike.User)
            .WithMany(user => user.LikesDislikes)
            .HasForeignKey(likeDislike => likeDislike.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(likeDislike => likeDislike.Comment)
            .WithMany(c => c.LikesDislikes)
            .HasForeignKey(likeDislike => likeDislike.CommentId);
    }
}