namespace FashionClothesAndTrends.Application.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; } = string.Empty;
        public Guid DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; } = null!;
    }
}
