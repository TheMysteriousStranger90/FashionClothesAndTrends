namespace FashionClothesAndTrends.Application.DTOs;

public class OrderItemHistoryDto
{
    public Guid ClothingItemId { get; set; }
    public string ClothingItemName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}
