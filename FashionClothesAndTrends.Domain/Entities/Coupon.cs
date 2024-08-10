using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; }
    public int DiscountPercentage { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
}