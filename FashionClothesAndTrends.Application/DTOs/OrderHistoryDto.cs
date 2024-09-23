﻿namespace FashionClothesAndTrends.Application.DTOs;

public class OrderHistoryDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
    public List<OrderItemHistoryDto> OrderItems { get; set; }
}
