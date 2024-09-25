namespace FashionClothesAndTrends.Application.DTOs;

public class CreateCouponDto
{
    public int DiscountPercentage { get; set; }
    public DateTime ExpiryDate { get; set; }
}