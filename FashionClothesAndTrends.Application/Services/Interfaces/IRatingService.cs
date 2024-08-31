using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IRatingService
{
    Task AddRatingAsync(Guid clothingItemId, RatingDto rating);
    Task<double?> GetAverageRatingAsync(Guid clothingItemId);
    Task UpdateRatingAsync(RatingDto ratingDto);
}