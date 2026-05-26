using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class UserPhoto : BaseEntity
{
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public required string PublicId { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}
