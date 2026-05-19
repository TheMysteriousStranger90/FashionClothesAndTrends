using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist>
{
    Task<Wishlist> CreateNewWishlistAsync(string userId, string wishlistName);
    Task RemoveWishlistAsync(Wishlist wishlist);
    Task<Wishlist?> GetWishlistByNameAsync(string userId, string wishlistName);
    Task<IReadOnlyList<Wishlist>> GetWishlistsByUserIdAsync(string userId);
    Task<IReadOnlyList<Wishlist>> GetWishlistsByClothingItemIdAsync(Guid clothingItemId);
    Task<WishlistItem> AddItemToWishlistAsync(Wishlist wishlist, Guid clothingItemId);
    Task<bool> RemoveItemFromWishlistAsync(Wishlist wishlist, Guid itemId);
}