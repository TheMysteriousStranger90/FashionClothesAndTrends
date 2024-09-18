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

    public async Task AddLikeToCommentAsync(LikeDislike likeDislike)
    {
        var _likeDislike = new LikeDislike
        {
            UserId = likeDislike.UserId,
            Comment = likeDislike.Comment,
            CommentId = likeDislike.CommentId,
            IsLike = likeDislike.IsLike,
            CreatedAt = DateTime.Now,
        };
        await _context.LikesDislikes.AddAsync(_likeDislike);
        await _context.SaveChangesAsync();
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
            .ToListAsync();    }
    
    public async Task<int> CountLikesAsync(Guid commentId)
    {
        return await _context.LikesDislikes
            .Where(ld => ld.CommentId == commentId && ld.IsLike)
            .CountAsync();
    }

    public async Task<int> CountDislikesAsync(Guid commentId)
    {
        return await _context.LikesDislikes
            .Where(ld => ld.CommentId == commentId && !ld.IsLike)
            .CountAsync();
    }
}