using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class Comment : BaseEntity
{
    public required string Text { get; set; }

    public required string UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; } = null!;

    public ICollection<LikeDislike> LikesDislikes { get; set; } = [];
}
