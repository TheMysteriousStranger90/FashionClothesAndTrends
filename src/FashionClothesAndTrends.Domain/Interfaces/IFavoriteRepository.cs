using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IFavoriteItemRepository : IGenericRepository<FavoriteItem>
{
    Task<IEnumerable<FavoriteItem>> GetFavoritesByUserIdAsync(string userId);
    Task<bool> IsFavoriteAsync(Guid clothingItemId, string userId);
    Task<FavoriteItem?> GetByClothingItemIdAndUserIdAsync(Guid clothingItemId, string userId);
}
