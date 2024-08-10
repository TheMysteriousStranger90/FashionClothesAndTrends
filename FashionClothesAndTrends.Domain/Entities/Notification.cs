using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class Notification : BaseEntity
{
    public string Text { get; set; }
    public bool IsRead { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}