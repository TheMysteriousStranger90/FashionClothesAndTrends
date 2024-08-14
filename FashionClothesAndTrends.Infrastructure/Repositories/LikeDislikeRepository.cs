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
}