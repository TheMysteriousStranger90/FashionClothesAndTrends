namespace FashionClothesAndTrends.Application.DTOs;

public class WishlistDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<WishlistItemDto> Items { get; set; }
}
