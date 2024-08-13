using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;
    
public class Wishlist : BaseEntity
{
    public string UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public ICollection<WishlistItem> Items { get; set; }
}