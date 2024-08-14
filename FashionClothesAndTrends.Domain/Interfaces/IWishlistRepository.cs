using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist>
{
    Task<IReadOnlyList<Wishlist>> GetWishlistsByUserIdAsync(string userId);
    Task<Wishlist?> GetWishlistByNameAsync(string userId, string name);
}
