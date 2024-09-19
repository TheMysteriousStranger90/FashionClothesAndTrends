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

    public async Task<WishlistDto> CreateWishlistAsync(string userId, string name)
    {
        var existingWishlist = await _unitOfWork.WishlistRepository.GetWishlistByNameAsync(userId, name);
        if (existingWishlist != null)
        {
            throw new ConflictException($"Wishlist with name '{name}' already exists for user '{userId}'.");
        }

        var wishlist = await _unitOfWork.WishlistRepository.CreateNewWishlistAsync(userId, name);
        return _mapper.Map<WishlistDto>(wishlist);
    }

    public async Task<bool> DeleteWishlistAsync(Guid wishlistId)
    {
        var wishlist = await _unitOfWork.WishlistRepository.GetByIdAsync(wishlistId);
        if (wishlist == null)
        {
            throw new NotFoundException($"Wishlist with ID '{wishlistId}' not found.");
        }

        await _unitOfWork.WishlistRepository.RemoveWishlistAsync(wishlist);
        return true;
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

    public async Task<WishlistItemDto> AddItemToWishlistAsync(string userId, Guid clothingItemId,
        string? wishlistName = null)
    {
        Wishlist wishlist;

        if (string.IsNullOrEmpty(wishlistName))
        {
            wishlist = await _unitOfWork.WishlistRepository.GetWishlistByNameAsync(userId, "Default");
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId,
                    Name = "Default",
                    Items = new List<WishlistItem>()
                };
                await _unitOfWork.WishlistRepository.AddAsync(wishlist);
            }
        }
        else
        {
            wishlist = await _unitOfWork.WishlistRepository.GetWishlistByNameAsync(userId, wishlistName);
            if (wishlist == null)
            {
                throw new NotFoundException($"Wishlist with name '{wishlistName}' not found for user '{userId}'.");
            }
        }

        var wishlistItem = await _unitOfWork.WishlistRepository.AddItemToWishlistAsync(wishlist, clothingItemId);
        return _mapper.Map<WishlistItemDto>(wishlistItem);
    }

    public async Task<bool> RemoveItemFromWishlistAsync(Guid wishlistId, Guid itemId)
    {
        var wishlist = await _unitOfWork.WishlistRepository.GetByIdAsync(wishlistId);
        if (wishlist == null)
        {
            throw new NotFoundException($"Wishlist with ID '{wishlistId}' not found.");
        }

        var result = await _unitOfWork.WishlistRepository.RemoveItemFromWishlistAsync(wishlist, itemId);
        if (!result)
        {
            throw new NotFoundException($"Item with ID '{itemId}' not found in wishlist '{wishlistId}'.");
        }

        return true;
    }
}