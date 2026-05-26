namespace FashionClothesAndTrends.Application.DTOs;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public Guid ClothingItemId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? TimeAgo { get; set; }
    public List<LikeDislikeDto>? LikesDislikes { get; set; }
}
