using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;

namespace FashionClothesAndTrends.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }
    public Role Role { get; set; }
    public ShippingAddress Address { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<OrderHistory> OrderHistories { get; set; } 
    public virtual ICollection<FavoriteItem> FavoriteItems { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; }
    public virtual ICollection<LikeDislike> LikesDislikes { get; set; }
    public virtual ICollection<Wishlist> Wishlists { get; set; }
}
