namespace FashionClothesAndTrends.Application.DTOs;

public class OrderUpdateDto
{
    public Guid? DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
    public List<OrderItemUpdateDto> OrderItems { get; set; }
    public string Status { get; set; }
    public decimal? Subtotal { get; set; }
}