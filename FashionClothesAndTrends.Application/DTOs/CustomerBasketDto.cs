﻿using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Application.DTOs;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; }
    public string? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public decimal ShippingPrice { get; set; }
}