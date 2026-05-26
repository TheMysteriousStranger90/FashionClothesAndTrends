using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Entities.Enums;

namespace FashionClothesAndTrends.Domain.Entities.OrderAggregate;

public class OrderHistory : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public required string ShippingAddress { get; set; }
    public ICollection<OrderItemHistory> OrderItems { get; set; } = [];
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
}
