using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IWishlistService
{
    Task<IReadOnlyList<WishlistDto>> GetWishlistsByUserIdAsync(string userId);
    Task<WishlistDto?> GetWishlistByNameAsync(string userId, string name);
    Task<WishlistDto> CreateWishlistAsync(string userId, string name);
    Task<bool> DeleteWishlistAsync(Guid wishlistId);
    Task<WishlistItemDto> AddItemToWishlistAsync(Guid wishlistId, Guid clothingItemId);
    Task<bool> RemoveItemFromWishlistAsync(Guid wishlistId, Guid itemId);
}