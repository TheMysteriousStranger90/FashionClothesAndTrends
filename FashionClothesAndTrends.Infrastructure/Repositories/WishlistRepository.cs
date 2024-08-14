using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class WishlistRepository : GenericRepository<Wishlist>, IWishlistRepository
{
    public WishlistRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Wishlist>> GetWishlistsByUserIdAsync(string userId)
    {
        return await _context.Wishlists
            .Where(w => w.UserId == userId)
            .Include(w => w.Items)
            .ToListAsync();
    }

    public async Task<Wishlist?> GetWishlistByNameAsync(string userId, string name)
    {
        return await _context.Wishlists
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == userId && w.Name == name);
    }
}
