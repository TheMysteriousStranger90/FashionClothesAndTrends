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
    
    public async Task<IEnumerable<CouponDto>> GetAllCouponsAsync()
    {
        var coupons = await _unitOfWork.CouponRepository.GetAllCouponsAsync();
        if (coupons == null || !coupons.Any())
        {
            throw new NotFoundException("No coupons found.");
        }

        var couponsDto = _mapper.Map<IEnumerable<CouponDto>>(coupons);
        return couponsDto;
    }

    public async Task CreateCouponAsync(CreateCouponDto couponDto)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var couponCode = new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        
        var coupon = _mapper.Map<Coupon>(couponDto);
        coupon.Code = couponCode;
        coupon.IsActive = true;
        
        await _unitOfWork.CouponRepository.CreateCouponAsync(coupon);
    }

    public async Task ApplyCouponToClothingItemAsync(Guid clothingItemId, Guid couponCodeId)
    {
        var clothingItem = await _unitOfWork.ClothingItemRepository.GetClothingByIdAsync(clothingItemId);
        if (clothingItem == null) throw new NotFoundException("Clothing item not found");

        var coupon = await _unitOfWork.CouponRepository.GetByIdAsync(couponCodeId);
        if (coupon == null || !coupon.IsActive || coupon.ExpiryDate <= DateTime.Now)
        {
            throw new NotFoundException("Coupon not found or expired");
        }

        clothingItem.Discount = coupon.DiscountPercentage;
        await _unitOfWork.SaveAsync();

        var wishlists = await _unitOfWork.WishlistRepository.GetWishlistsByClothingItemIdAsync(clothingItemId);
        
        /*
        var usersToNotify = wishlists
            .Where(w => w.Items.Any(wi => wi.ClothingItemId == clothingItemId))
            .Select(w => w.UserId)
            .ToList();

        foreach (var userId in usersToNotify)
        {
            await _notificationService.NotifyUserAboutDiscountAsync(userId, clothingItemId);
        }
        */
    }
}