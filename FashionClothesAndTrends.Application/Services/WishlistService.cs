using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;

namespace FashionClothesAndTrends.Application.Services;

public class WishlistService : IWishlistService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WishlistService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<WishlistDto>> GetWishlistsByUserIdAsync(string userId)
    {
        var wishlists = await _unitOfWork.WishlistRepository.GetWishlistsByUserIdAsync(userId);
        return _mapper.Map<IReadOnlyList<WishlistDto>>(wishlists);
    }

    public async Task<WishlistDto?> GetWishlistByNameAsync(string userId, string name)
    {
        var wishlist = await _unitOfWork.WishlistRepository.GetWishlistByNameAsync(userId, name);
        if (wishlist == null)
        {
            throw new NotFoundException($"Wishlist with name '{name}' not found for user '{userId}'.");
        }

        return _mapper.Map<WishlistDto>(wishlist);
    }

    public async Task<WishlistDto> CreateWishlistAsync(string userId, string name)
    {
        var existingWishlist = await _unitOfWork.WishlistRepository.GetWishlistByNameAsync(userId, name);
        if (existingWishlist != null)
        {
            throw new ConflictException($"Wishlist with name '{name}' already exists for user '{userId}'.");
        }

        var wishlist = new Wishlist
        {
            UserId = userId,
            Name = name,
            Items = new List<WishlistItem>()
        };

        await _unitOfWork.WishlistRepository.AddAsync(wishlist);
        return _mapper.Map<WishlistDto>(wishlist);
    }

    public async Task<bool> DeleteWishlistAsync(Guid wishlistId)
    {
        var wishlist = await _unitOfWork.WishlistRepository.GetByIdAsync(wishlistId);
        if (wishlist == null)
        {
            throw new NotFoundException($"Wishlist with ID '{wishlistId}' not found.");
        }

        await _unitOfWork.WishlistRepository.RemoveAsync(wishlist);
        return true;
    }

    public async Task<WishlistItemDto> AddItemToWishlistAsync(Guid wishlistId, Guid clothingItemId)
    {
        var wishlist = await _unitOfWork.WishlistRepository.GetByIdAsync(wishlistId);
        if (wishlist == null)
        {
            throw new NotFoundException($"Wishlist with ID '{wishlistId}' not found.");
        }

        var wishlistItem = new WishlistItem
        {
            WishlistId = wishlistId,
            ClothingItemId = clothingItemId
        };

        wishlist.Items.Add(wishlistItem);
        await _unitOfWork.WishlistRepository.UpdateAsync(wishlist);

        return _mapper.Map<WishlistItemDto>(wishlistItem);
    }

    public async Task<bool> RemoveItemFromWishlistAsync(Guid wishlistId, Guid itemId)
    {
        var wishlist = await _unitOfWork.WishlistRepository.GetByIdAsync(wishlistId);
        if (wishlist == null)
        {
            throw new NotFoundException($"Wishlist with ID '{wishlistId}' not found.");
        }

        var item = wishlist.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new NotFoundException($"Item with ID '{itemId}' not found in wishlist '{wishlistId}'.");
        }

        wishlist.Items.Remove(item);
        await _unitOfWork.WishlistRepository.UpdateAsync(wishlist);

        return true;
    }
}