using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddRatingToClothingItemAsync(Rating rating)
    {
        var existingRating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.UserId == rating.UserId && r.ClothingItemId == rating.ClothingItemId);

        if (existingRating != null)
        {
            existingRating.Score = rating.Score;
            existingRating.LastUpdatedAt = DateTime.Now;
        }
        else
        {
            var _rating = new Rating
            {
                UserId = rating.UserId,
                ClothingItem = rating.ClothingItem,
                ClothingItemId = rating.ClothingItemId,
                Score = rating.Score,
                CreatedAt = DateTime.Now
            };
            await _context.Ratings.AddAsync(_rating);
        }
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateRatingAsync(string userId, Guid clothingItemId, int value)
    {
        var rating = await _context.Ratings
            .FirstOrDefaultAsync(r => r.UserId == userId && r.ClothingItemId == clothingItemId);

        if (rating != null)
        {
            rating.Score = value;
            rating.CreatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Rating>> GetRatingsByUserIdAsync(string userId)
    {
        return await _context.Ratings
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rating>> GetRatingsByClothingItemIdAsync(Guid clothingItemId)
    {
        return await _context.Ratings
            .Where(r => r.ClothingItemId == clothingItemId)
            .ToListAsync();
    }

    public async Task<double?> GetAverageRatingByClothingItemIdAsync(Guid clothingItemId)
    {
        var ratings = await _context.Ratings
            .Where(r => r.ClothingItemId == clothingItemId)
            .ToListAsync();

        if (!ratings.Any())
        {
            return 0;
        }

        return ratings.Average(r => r.Score);
    }
    
    public async Task<Rating?> GetUserRatingAsync(string userId, Guid clothingItemId)
    {
        return await _context.Ratings
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.UserId == userId && r.ClothingItemId == clothingItemId);
    }
}