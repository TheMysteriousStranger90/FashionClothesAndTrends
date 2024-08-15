namespace FashionClothesAndTrends.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid ClothingItemId { get; set; }
        public string ClothingItemName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}