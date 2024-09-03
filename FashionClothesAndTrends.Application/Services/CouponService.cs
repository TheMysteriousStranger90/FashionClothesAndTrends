using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Application.Services;

public class CouponService : ICouponService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public CouponService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<CouponDto> CreateCouponAsync(CouponDto couponDto)
    {
        var coupon = _mapper.Map<Coupon>(couponDto);
        await _unitOfWork.GenericRepository<Coupon>().AddAsync(coupon);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<CouponDto>(coupon);
    }

    public async Task ApplyCouponToClothingItemAsync(Guid clothingItemId, string couponCode)
    {
        var clothingItem = await _unitOfWork.ClothingItemRepository.GetClothingByIdAsync(clothingItemId);
        if (clothingItem == null) throw new NotFoundException("Clothing item not found");

        var coupon = await _unitOfWork.GenericRepository<Coupon>()
            .GetByConditionAsync(c => c.Code == couponCode && c.IsActive && c.ExpiryDate > DateTime.Now);
        if (coupon == null) throw new NotFoundException("Coupon not found or expired");

        clothingItem.Discount = coupon.DiscountPercentage;
        await _unitOfWork.SaveAsync();

        var wishlists = await _unitOfWork.WishlistRepository.GetWishlistsByClothingItemIdAsync(clothingItemId);
        var usersToNotify = wishlists
            .Where(w => w.Items.Any(wi => wi.ClothingItemId == clothingItemId))
            .Select(w => w.UserId)
            .ToList();

        foreach (var userId in usersToNotify)
        {
            await _notificationService.NotifyUserAboutDiscountAsync(userId, clothingItemId);
        }
    }
}