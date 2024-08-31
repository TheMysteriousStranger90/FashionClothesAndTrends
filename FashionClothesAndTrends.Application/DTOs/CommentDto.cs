namespace FashionClothesAndTrends.Application.DTOs;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string Username { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<LikeDislikeDto> LikesDislikes { get; set; }
}
