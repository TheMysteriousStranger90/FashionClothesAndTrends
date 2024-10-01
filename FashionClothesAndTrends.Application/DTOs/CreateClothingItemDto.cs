namespace FashionClothesAndTrends.Application.DTOs;

public class CreateClothingItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Gender { get; set; }
    public string Size { get; set; }
    public string Category { get; set; }
    public bool IsInStock { get; set; }
    public string? PictureUrl { get; set; }
    public string Brand { get; set; }
}