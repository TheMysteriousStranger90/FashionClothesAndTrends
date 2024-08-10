using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class FavoriteItem : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; }
}