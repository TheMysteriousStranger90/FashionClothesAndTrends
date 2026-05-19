namespace FashionClothesAndTrends.Application.DTOs;

public class OrderHistoryDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public List<OrderItemHistoryDto> OrderItems { get; set; } = [];
}
