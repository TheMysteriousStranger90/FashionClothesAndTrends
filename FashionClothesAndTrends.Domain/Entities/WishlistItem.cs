using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class WishlistItem : BaseEntity
{
    public Guid WishlistId { get; set; }
    public Wishlist Wishlist { get; set; }
    public Guid ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; }
}