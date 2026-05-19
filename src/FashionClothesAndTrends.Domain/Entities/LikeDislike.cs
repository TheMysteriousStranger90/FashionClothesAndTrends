using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class LikeDislike : BaseEntity
{
    public bool IsLike { get; set; }

    public Guid CommentId { get; set; }
    public Comment Comment { get; set; } = null!;

    public required string UserId { get; set; }
    public User User { get; set; } = null!;
}