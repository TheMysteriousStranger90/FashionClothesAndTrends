namespace FashionClothesAndTrends.Application.DTOs;

public class OrderUpdateDto
{
    public Guid? DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; } = null!;
    public List<OrderItemUpdateDto> OrderItems { get; set; } = [];
    public string Status { get; set; } = string.Empty;
    public decimal? Subtotal { get; set; }
}
