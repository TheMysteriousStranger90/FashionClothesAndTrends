using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class FavoriteItemRepository : GenericRepository<FavoriteItem>, IFavoriteItemRepository
{
    public FavoriteItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FavoriteItem>> GetFavoritesByUserIdAsync(string userId)
    {
        return await _context.FavoriteItems
            .Include(f => f.ClothingItem)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> IsFavoriteAsync(Guid clothingItemId, string userId)
    {
        return await _context.FavoriteItems
            .AnyAsync(f => f.ClothingItemId == clothingItemId && f.UserId == userId);
    }

    public async Task<FavoriteItem> GetByClothingItemIdAndUserId(Guid clothingItemId, string userId)
    {
        return await _context.FavoriteItems
            .Include(f => f.ClothingItem)
            .FirstOrDefaultAsync(f => f.ClothingItemId == clothingItemId && f.UserId == userId);
    }
}