using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class Wishlist : BaseEntity
{
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
    public required string Name { get; set; }
    public ICollection<WishlistItem> Items { get; set; } = [];
}