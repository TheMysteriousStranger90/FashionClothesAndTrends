using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Application.DTOs;

public class BasketItemDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string ClothingName { get; set; }

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Required]
    public string PictureUrl { get; set; }

    [Required]
    public string Brand { get; set; }
    
    public decimal? Discount { get; set; }
}