using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Application.DTOs;

public class RatingDto
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public Guid ClothingItemId { get; set; }
    [Range(1,5)]
    public int Score { get; set; }
}
