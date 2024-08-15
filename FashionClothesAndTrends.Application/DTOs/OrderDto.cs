namespace FashionClothesAndTrends.Application.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}