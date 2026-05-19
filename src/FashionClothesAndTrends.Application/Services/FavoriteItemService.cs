using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services;

public class FavoriteItemService : IFavoriteItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FavoriteItemService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddFavoriteAsync(Guid clothingItemId, string userId)
    {
        if (await _unitOfWork.FavoriteItemRepository.IsFavoriteAsync(clothingItemId, userId))
        {
            throw new ConflictException("This item is already in the favorites.");
        }

        var favoriteItem = new FavoriteItem
        {
            UserId = userId,
            ClothingItemId = clothingItemId
        };

        await _unitOfWork.FavoriteItemRepository.AddAsync(favoriteItem);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoveFavoriteAsync(Guid clothingItemId, string userId)
    {
        var favoriteItem = await _unitOfWork.FavoriteItemRepository.GetByClothingItemIdAndUserId(clothingItemId, userId);
        if (favoriteItem == null)
        {
            throw new NotFoundException("Favorite item not found.");
        }

        _unitOfWork.FavoriteItemRepository.Remove(favoriteItem);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<FavoriteItemDto>> GetFavoritesByUserIdAsync(string userId)
    {
        var favoriteItems = await _unitOfWork.FavoriteItemRepository.GetFavoritesByUserIdAsync(userId);
        if (favoriteItems == null || !favoriteItems.Any())
        {
            throw new NotFoundException("No favorite items found for this user.");
        }

        return _mapper.Map<IEnumerable<FavoriteItemDto>>(favoriteItems);
    }

    public async Task<bool> IsFavoriteAsync(Guid clothingItemId, string userId)
    {
        return await _unitOfWork.FavoriteItemRepository.IsFavoriteAsync(clothingItemId, userId);
    }
}