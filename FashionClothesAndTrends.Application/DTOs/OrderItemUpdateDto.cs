namespace FashionClothesAndTrends.Application.DTOs;

public class OrderItemUpdateDto
{
    public Guid ClothingItemId { get; set; }
    public string ClothingItemName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}