using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities.OrderAggregate;

public class OrderItemHistory : BaseEntity
{
    public Guid OrderHistoryId { get; set; }
    public Guid ClothingItemId { get; set; }
    public string ClothingItemName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}