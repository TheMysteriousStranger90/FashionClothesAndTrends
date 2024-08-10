using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class ClothingBrand : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<ClothingItem> ClothingItems { get; set; }
}