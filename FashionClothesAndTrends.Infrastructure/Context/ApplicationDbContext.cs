using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using FashionClothesAndTrends.Infrastructure.Configurations;
using FashionClothesAndTrends.Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<User, AppRole, string,
    IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ClothingItem> ClothingItems { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<FavoriteItem> FavoriteItems { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<LikeDislike> LikesDislikes { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }
    public DbSet<OrderItemHistory> OrderItemHistories { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ClothingItemConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new RatingConfiguration());
        modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
        modelBuilder.ApplyConfiguration(new LikeDislikeConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryMethodConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());

        SeedDataInitializer.ContextSeed(modelBuilder);
    }
}