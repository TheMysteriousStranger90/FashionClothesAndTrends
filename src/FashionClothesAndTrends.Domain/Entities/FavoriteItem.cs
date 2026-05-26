using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class FavoriteItem : BaseEntity
{
    public required string UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; } = null!;
}
