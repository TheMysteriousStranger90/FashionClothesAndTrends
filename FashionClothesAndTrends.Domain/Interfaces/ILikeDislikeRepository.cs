using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface ILikeDislikeRepository : IGenericRepository<LikeDislike>
{
    Task<IEnumerable<LikeDislike>> GetLikesDislikesByUserIdAsync(string userId);
    Task<IEnumerable<LikeDislike>> GetLikesDislikesByCommentIdAsync(Guid commentId);
}