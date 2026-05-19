namespace FashionClothesAndTrends.Application.DTOs;

public class ClothingItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal? Discount { get; set; }
    public bool IsInStock { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public List<ClothingItemPhotoDto> ClothingItemPhotos { get; set; } = [];
}
