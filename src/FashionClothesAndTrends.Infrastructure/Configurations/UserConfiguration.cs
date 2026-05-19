using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionClothesAndTrends.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.Address, a =>
        {
            a.WithOwner();

            a.Property(sa => sa.AddressLine)
                .IsRequired(false)
                .HasMaxLength(200);

            a.Property(sa => sa.City)
                .IsRequired(false)
                .HasMaxLength(100);

            a.Property(sa => sa.State)
                .IsRequired(false)
                .HasMaxLength(100);

            a.Property(sa => sa.PostalCode)
                .IsRequired(false)
                .HasMaxLength(20);

            a.Property(sa => sa.Country)
                .IsRequired(false)
                .HasMaxLength(100);

            a.Property(sa => sa.IsDefault)
                .IsRequired(false)
                .HasDefaultValue(false);

            a.HasOne(sa => sa.User)
                .WithOne(u => u.Address)
                .HasForeignKey<ShippingAddress>(sa => sa.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.HasMany(a => a.UserRoles).WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Property(x => x.DateOfBirth)
            .HasConversion(new DateOnlyToDateTimeConverter());

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(u => u.Orders)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.OrderHistories)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.FavoriteItems)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Ratings)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Comments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Notifications)
            .WithOne(notification => notification.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.LikesDislikes)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Wishlists)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}