using FashionClothesAndTrends.Application.DTOs;

namespace FashionClothesAndTrends.Application.Services.Interfaces;

public interface ICouponService
{
    Task CreateCouponAsync(CreateCouponDto createCouponDto);
    Task ApplyCouponToClothingItemAsync(Guid clothingItemId, Guid couponCodeId);
    Task<IEnumerable<CouponDto>> GetAllCouponsAsync();
}