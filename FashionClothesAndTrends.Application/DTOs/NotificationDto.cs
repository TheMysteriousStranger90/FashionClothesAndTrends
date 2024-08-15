namespace FashionClothesAndTrends.Application.DTOs;

public class NotificationDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}
