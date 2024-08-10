using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class LikeDislike : BaseEntity
{
    public bool IsLike { get; set; }

    public Guid CommentId { get; set; }
    public Comment Comment { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}