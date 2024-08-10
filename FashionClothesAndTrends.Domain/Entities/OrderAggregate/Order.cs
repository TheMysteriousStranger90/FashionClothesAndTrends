using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Entities.Enums;

namespace FashionClothesAndTrends.Domain.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, AddressAggregate shipToAddress,
        DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId, Coupon coupon = null)
    {
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Subtotal = subtotal;
        PaymentIntentId = paymentIntentId;
        Coupon = coupon;
    }

    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public AddressAggregate ShipToAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public IReadOnlyList<OrderItem> OrderItems { get; set; }
    public Coupon Coupon { get; set; }
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string PaymentIntentId { get; set; }

    public decimal GetTotal()
    {
        decimal discount = Coupon != null && Coupon.IsActive && Coupon.ExpiryDate > DateTime.Now
            ? Subtotal * Coupon.DiscountPercentage / 100
            : 0;

        return Subtotal + DeliveryMethod.Price - discount;
    }
}
