namespace FashionClothesAndTrends.Application.DTOs;

public class WishlistItemDto
{
    public Guid Id { get; set; }
    public string ClothingItemName { get; set; }
    public Guid ClothingItemId { get; set; }
    public string PictureUrl { get; set; }
}
