using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services;

public class LikeDislikeService : ILikeDislikeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LikeDislikeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddLikeDislikeAsync(LikeDislikeDto likeDislikeDto)
    {
        if (likeDislikeDto == null)
        {
            throw new ArgumentNullException(nameof(likeDislikeDto));
        }

        var likeDislike = _mapper.Map<LikeDislike>(likeDislikeDto);
        await _unitOfWork.LikeDislikeRepository.AddLikeToCommentAsync(likeDislike);
    }

    public async Task RemoveLikeDislikeAsync(Guid likeDislikeId)
    {
        var likeDislike = await _unitOfWork.LikeDislikeRepository.GetByIdAsync(likeDislikeId);
        if (likeDislike == null)
        {
            throw new NotFoundException("Like/Dislike not found.");
        }

        _unitOfWork.LikeDislikeRepository.Remove(likeDislike);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<LikeDislikeDto>> GetLikesDislikesByUserIdAsync(string userId)
    {
        var likesDislikes = await _unitOfWork.LikeDislikeRepository.GetLikesDislikesByUserIdAsync(userId);
        if (likesDislikes == null || !likesDislikes.Any())
        {
            throw new NotFoundException("No likes/dislikes found for this user.");
        }

        return _mapper.Map<IEnumerable<LikeDislikeDto>>(likesDislikes);
    }

    public async Task<IEnumerable<LikeDislikeDto>> GetLikesDislikesByCommentIdAsync(Guid commentId)
    {
        var likesDislikes = await _unitOfWork.LikeDislikeRepository.GetLikesDislikesByCommentIdAsync(commentId);
        if (likesDislikes == null || !likesDislikes.Any())
        {
            throw new NotFoundException("No likes/dislikes found for this comment.");
        }

        return _mapper.Map<IEnumerable<LikeDislikeDto>>(likesDislikes);
    }
    
    public async Task<int> CountLikesAsync(Guid commentId)
    {
        return await _unitOfWork.LikeDislikeRepository.CountLikesAsync(commentId);
    }

    public async Task<int> CountDislikesAsync(Guid commentId)
    {
        return await _unitOfWork.LikeDislikeRepository.CountDislikesAsync(commentId);
    }
}