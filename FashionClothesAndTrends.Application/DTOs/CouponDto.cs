namespace FashionClothesAndTrends.Application.DTOs;

public class CouponDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public int DiscountPercentage { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
}
