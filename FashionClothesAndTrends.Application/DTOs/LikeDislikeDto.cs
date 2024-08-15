namespace FashionClothesAndTrends.Application.DTOs;

public class LikeDislikeDto
{
    public bool IsLike { get; set; }

    public Guid CommentId { get; set; }
    public CommentDto CommentDto { get; set; }

    public string UserDtoId { get; set; }
    public UserDto UserDto { get; set; }
}