using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Extensions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddCommentAsync(CommentDto commentDto)
    {
        if (commentDto == null)
        {
            throw new ArgumentNullException(nameof(commentDto));
        }
        
        var user = await _unitOfWork.UserManager.FindByIdAsync(commentDto.UserId);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        var comment = _mapper.Map<Comment>(commentDto);
        await _unitOfWork.CommentRepository.AddCommentToClothingItemAsync(comment);
    }

    public async Task RemoveCommentAsync(Guid commentId, string userId)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
        if (comment == null)
        {
            throw new NotFoundException("Comment not found.");
        }
        
        var currentUser = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
        if (currentUser == null)
        {
            throw new NotFoundException("User not found.");
        }

        var isAdmin = await _unitOfWork.UserManager.IsInRoleAsync(currentUser, "Administrator");
        
        if (comment.UserId != currentUser.Id && !isAdmin)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this comment.");
        }
        
        await _unitOfWork.CommentRepository.RemoveCommentAsync(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsForClothingItemAsync(Guid clothingItemId)
    {
        var comments = await _unitOfWork.CommentRepository.GetCommentsForClothingItemIdAsync(clothingItemId);
        if (comments == null || !comments.Any())
        {
            throw new NotFoundException("No comments found for this clothing item.");
        }

        var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
        foreach (var commentDto in commentDtos)
        {
            commentDto.TimeAgo = commentDto.CreatedAt.DateTimeAgo();
        }

        return commentDtos;
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByUserIdAsync(string userId)
    {
        var comments = await _unitOfWork.CommentRepository.GetCommentsByUserIdAsync(userId);
        if (comments == null || !comments.Any())
        {
            throw new NotFoundException("No comments found for this user.");
        }

        var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
        foreach (var commentDto in commentDtos)
        {
            commentDto.TimeAgo = commentDto.CreatedAt.DateTimeAgo();
        }

        return commentDtos;
    }
}