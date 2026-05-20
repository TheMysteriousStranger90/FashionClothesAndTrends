using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task AddCommentToClothingItemAsync(Comment comment)
    {
        comment.CreatedAt = DateTime.UtcNow;
        _context.Comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task RemoveCommentAsync(Comment comment)
    {
        _context.Comments.Remove(comment);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Comment>> GetCommentsForClothingItemIdAsync(Guid clothingItemId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Where(c => c.ClothingItemId == clothingItemId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(string userId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }
}
