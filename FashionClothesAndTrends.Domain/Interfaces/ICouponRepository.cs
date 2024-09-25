using FashionClothesAndTrends.Domain.Entities;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<IReadOnlyList<Coupon>> GetAllCouponsAsync();
    Task CreateCouponAsync(Coupon coupon);
}