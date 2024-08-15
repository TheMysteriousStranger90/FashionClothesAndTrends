namespace FashionClothesAndTrends.Application.DTOs;

public class FavoriteItemDto
{
    public string UserDtoId { get; set; }
    public UserDto UserDto { get; set; }

    public Guid ClothingItemDtoId { get; set; }
    public ClothingItemDto ClothingItemDto { get; set; }
}