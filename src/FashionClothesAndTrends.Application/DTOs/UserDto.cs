namespace FashionClothesAndTrends.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public List<UserPhotoDto> UserPhotos { get; set; } = [];
    public string Token { get; set; } = string.Empty;
}
