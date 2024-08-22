using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class ClothingItemPhoto : BaseEntity
{
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }
    
    public Guid ClothingItemId { get; set; }
    public ClothingItem ClothingItem { get; set; } 
}