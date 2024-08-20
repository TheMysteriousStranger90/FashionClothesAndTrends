using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface ICommentService
{
    Task AddCommentAsync(CommentDto commentDto);
    Task RemoveCommentAsync(Guid commentId);
    Task<IEnumerable<CommentDto>> GetCommentsForClothingItemAsync(Guid clothingItemId);
    Task<IEnumerable<CommentDto>> GetCommentsByUserIdAsync(string userId);
}