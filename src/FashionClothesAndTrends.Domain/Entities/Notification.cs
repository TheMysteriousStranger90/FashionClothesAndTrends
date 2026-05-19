using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class Notification : BaseEntity
{
    public required string Text { get; set; }
    public bool IsRead { get; set; }

    public required string UserId { get; set; }
    public User User { get; set; } = null!;
}