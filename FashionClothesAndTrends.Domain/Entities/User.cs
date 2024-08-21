using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;

namespace FashionClothesAndTrends.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public ShippingAddress Address { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
    public ICollection<UserPhoto?> UserPhotos { get; set; } = new List<UserPhoto?>();
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<OrderHistory> OrderHistories { get; set; }
    public virtual ICollection<FavoriteItem> FavoriteItems { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; }
    public virtual ICollection<LikeDislike> LikesDislikes { get; set; }
    public virtual ICollection<Wishlist> Wishlists { get; set; }
}
