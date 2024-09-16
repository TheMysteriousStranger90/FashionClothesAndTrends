using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface IRatingService
{
    Task AddRatingAsync(RatingDto rating);
    Task<double?> GetAverageRatingAsync(Guid clothingItemId);
    Task UpdateRatingAsync(RatingDto ratingDto);
    Task<RatingDto?> GetUserRatingAsync(string userId, Guid clothingItemId);
}