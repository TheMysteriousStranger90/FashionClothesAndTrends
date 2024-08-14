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

    public async Task AddRatingToClothingItemAsync(Guid clothingItemId, Rating rating)
    {
        rating.ClothingItemId = clothingItemId;
        await _context.Ratings.AddAsync(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<double?> GetAverageRatingByClothingItemIdAsync(Guid clothingItemId)
    {
        var ratings = await _context.Ratings
            .Where(r => r.ClothingItemId == clothingItemId)
            .ToListAsync();

        if (ratings.Any())
        {
            return ratings.Average(r => r.Score);
        }
        else
        {
            return null;
        }
    }
}