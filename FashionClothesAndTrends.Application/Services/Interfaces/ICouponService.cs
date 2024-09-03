using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface ICouponService
{
    Task<CouponDto> CreateCouponAsync(CouponDto couponDto);
    Task ApplyCouponToClothingItemAsync(Guid clothingItemId, string couponCode);
}