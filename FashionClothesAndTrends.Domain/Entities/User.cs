using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Entities.Enums;

namespace FashionClothesAndTrends.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
    public ICollection<FavoriteItem> FavoriteItems { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<LikeDislike> LikesDislikes { get; set; }
}
