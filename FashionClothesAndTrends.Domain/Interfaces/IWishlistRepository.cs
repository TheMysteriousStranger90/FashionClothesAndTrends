using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IWishlistRepository : IGenericRepository<Wishlist>
{
    Task<IReadOnlyList<Wishlist>> GetWishlistsByUserIdAsync(Guid userId);
    Task<Wishlist?> GetWishlistByNameAsync(Guid userId, string name);
}
