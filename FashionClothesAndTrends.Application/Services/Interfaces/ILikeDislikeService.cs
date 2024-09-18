using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface ILikeDislikeService
{
    Task AddLikeDislikeAsync(LikeDislikeDto likeDislikeDto);
    Task RemoveLikeDislikeAsync(Guid likeDislikeId);
    Task<IEnumerable<LikeDislikeDto>> GetLikesDislikesByUserIdAsync(string userId);
    Task<IEnumerable<LikeDislikeDto>> GetLikesDislikesByCommentIdAsync(Guid commentId);
    Task<int> CountLikesAsync(Guid commentId);
    Task<int> CountDislikesAsync(Guid commentId);
}