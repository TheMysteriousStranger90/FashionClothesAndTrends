using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Hubs;
using FashionClothesAndTrends.Application.Hubs.Interfaces;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace FashionClothesAndTrends.Application.Services;

public class CouponService : ICouponService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHubContext<DiscountNotificationHub, INotificationHub> _discountNotification;
    private readonly INotificationService _notificationService;

    public CouponService(IUnitOfWork unitOfWork, IMapper mapper,
        IHubContext<DiscountNotificationHub, INotificationHub> hubContext, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _discountNotification = hubContext;
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
        var usersToNotify = wishlists
            .Where(w => w.Items.Any(wi => wi.ClothingItemId == clothingItemId))
            .Select(w => w.UserId)
            .ToList();

        foreach (var userId in usersToNotify)
        {
            var notification = new Notification
            {
                Text =
                    $"A discount of {coupon.DiscountPercentage}% has been applied to an {clothingItem.Name} item in your wishlist.",
                UserId = userId,
                IsRead = false
            };

            await _notificationService.AddNotificationAsync(notification);

            await _discountNotification.Clients.Group(userId).SendMessage(notification);
        }
    }
}