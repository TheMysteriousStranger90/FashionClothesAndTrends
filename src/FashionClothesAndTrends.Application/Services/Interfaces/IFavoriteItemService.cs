using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IFavoriteItemService
{
    Task AddFavoriteAsync(Guid clothingItemId, string userId);
    Task RemoveFavoriteAsync(Guid clothingItemId, string userId);
    Task<IEnumerable<FavoriteItemDto>> GetFavoritesByUserIdAsync(string userId);
    Task<bool> IsFavoriteAsync(Guid clothingItemId, string userId);
}