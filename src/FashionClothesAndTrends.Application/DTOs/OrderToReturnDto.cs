using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;

public class OrderToReturnDto
{
    public Guid Id { get; set; }
    public string BuyerEmail { get; set; } = string.Empty;
    public DateTimeOffset OrderDate { get; set; }
    public AddressAggregate ShipToAddress { get; set; } = null!;
    public string DeliveryMethod { get; set; } = string.Empty;
    public decimal ShippingPrice { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
}
