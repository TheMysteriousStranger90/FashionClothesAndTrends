using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class LikeDislikeRepository : GenericRepository<LikeDislike>, ILikeDislikeRepository
{
    public LikeDislikeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task AddLikeToCommentAsync(LikeDislike likeDislike)
    {
        likeDislike.CreatedAt = DateTime.UtcNow;
        _context.LikesDislikes.Add(likeDislike);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<LikeDislike>> GetLikesDislikesByUserIdAsync(string userId)
    {
        return await _context.LikesDislikes
            .Include(ld => ld.Comment)
            .Where(ld => ld.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<LikeDislike>> GetLikesDislikesByCommentIdAsync(Guid commentId)
    {
        return await _context.LikesDislikes
            .Where(ld => ld.CommentId == commentId)
            .ToListAsync();
    }

    public async Task<int> CountLikesAsync(Guid commentId)
    {
        return await _context.LikesDislikes
            .CountAsync(ld => ld.CommentId == commentId && ld.IsLike);
    }

    public async Task<int> CountDislikesAsync(Guid commentId)
    {
        return await _context.LikesDislikes
            .CountAsync(ld => ld.CommentId == commentId && !ld.IsLike);
    }
}
