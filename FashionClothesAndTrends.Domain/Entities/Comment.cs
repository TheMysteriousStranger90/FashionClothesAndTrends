using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
    
    public Guid ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; }
    
    public ICollection<LikeDislike> LikesDislikes { get; set; }
}