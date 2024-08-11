using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IRatingRepository : IGenericRepository<Rating>
{
    Task AddRatingToClothingItemAsync(Guid clothingItemId, Rating rating);
    Task<double?> GetAverageRatingByClothingItemIdAsync(Guid clothingItemId);
}