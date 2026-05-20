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

    public Task<Wishlist> CreateNewWishlistAsync(string userId, string wishlistName)
    {
        var wishlist = new Wishlist
        {
            UserId = userId,
            Name = wishlistName,
            Items = [],
            CreatedAt = DateTime.UtcNow,
        };

        _context.Wishlists.Add(wishlist);
        return Task.FromResult(wishlist);
    }

    public Task RemoveWishlistAsync(Wishlist wishlist)
    {
        _context.Wishlists.Remove(wishlist);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Wishlist>> GetWishlistsByUserIdAsync(string userId)
    {
        return await _context.Wishlists
            .Where(w => w.UserId == userId)
            .Include(u => u.User)
            .Include(w => w.Items)
            .ThenInclude(i => i.ClothingItem)
            .ThenInclude(ci => ci.ClothingItemPhotos)
            .ToListAsync();
    }

    public async Task<Wishlist?> GetWishlistByNameAsync(string userId, string wishlistName)
    {
        return await _context.Wishlists
            .Include(w => w.Items)
            .Include(u => u.User)
            .FirstOrDefaultAsync(w => w.UserId == userId && w.Name == wishlistName);
    }

    public async Task<IReadOnlyList<Wishlist>> GetWishlistsByClothingItemIdAsync(Guid clothingItemId)
    {
        return await _context.Wishlists
            .Where(w => w.Items.Any(i => i.ClothingItemId == clothingItemId))
            .Include(w => w.Items)
            .Include(u => u.User)
            .ToListAsync();
    }

    public async Task<WishlistItem> AddItemToWishlistAsync(Wishlist wishlist, Guid clothingItemId)
    {
        var wishlistItem = new WishlistItem
        {
            WishlistId = wishlist.Id,
            ClothingItemId = clothingItemId
        };

        wishlist.Items.Add(wishlistItem);

        await _context.Entry(wishlistItem)
            .Reference(wi => wi.ClothingItem)
            .Query()
            .Include(ci => ci.ClothingItemPhotos)
            .LoadAsync();

        return wishlistItem;
    }

    public Task<bool> RemoveItemFromWishlistAsync(Wishlist wishlist, Guid itemId)
    {
        var item = wishlist.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            return Task.FromResult(false);
        }

        wishlist.Items.Remove(item);
        return Task.FromResult(true);
    }

    public override async Task<Wishlist?> GetByIdAsync(Guid id)
    {
        return await _context.Wishlists
            .Include(w => w.Items)
            .ThenInclude(i => i.ClothingItem)
            .ThenInclude(ci => ci.ClothingItemPhotos)
            .FirstOrDefaultAsync(w => w.Id == id);
    }
}
