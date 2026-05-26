using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Repositories;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    public CouponRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Coupon>> GetAllCouponsAsync()
    {
        return await _context.Coupons.ToListAsync();
    }

    public Task CreateCouponAsync(Coupon coupon)
    {
        _context.Coupons.Add(coupon);
        return Task.CompletedTask;
    }
}
