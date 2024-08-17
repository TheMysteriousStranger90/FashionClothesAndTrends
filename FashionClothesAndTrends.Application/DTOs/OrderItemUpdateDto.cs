namespace FashionClothesAndTrends.Application.DTOs;

public class OrderItemUpdateDto
{
    public Guid OrderItemId { get; set; }
    public Guid ClothingItemId { get; set; }
    public string ClothingItemName { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}