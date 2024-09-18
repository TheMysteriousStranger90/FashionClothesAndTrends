using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Application.DTOs;

public class LikeDislikeDto
{
    public bool IsLike { get; set; }

    [Required]
    public Guid CommentId { get; set; }

    [Required]
    public string UserId { get; set; }
    
    public string Username { get; set; }
}