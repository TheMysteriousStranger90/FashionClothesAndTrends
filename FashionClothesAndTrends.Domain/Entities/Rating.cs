using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class Rating : BaseEntity
{ 
    public int Score { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public Guid ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; }
}