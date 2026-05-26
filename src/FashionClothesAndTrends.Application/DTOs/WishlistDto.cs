namespace FashionClothesAndTrends.Application.DTOs;

public class WishlistDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public List<WishlistItemDto> Items { get; set; } = [];
}
