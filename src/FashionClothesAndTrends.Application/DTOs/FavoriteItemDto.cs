namespace FashionClothesAndTrends.Application.DTOs;

public class FavoriteItemDto
{
    public string UserDtoId { get; set; } = string.Empty;
    public UserDto UserDto { get; set; } = null!;

    public Guid ClothingItemDtoId { get; set; }
    public ClothingItemDto ClothingItemDto { get; set; } = null!;
}
