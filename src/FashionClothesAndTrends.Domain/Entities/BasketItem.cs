using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class BasketItem : BaseEntity
{
    public string ClothingName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal? Discount { get; set; }
}
