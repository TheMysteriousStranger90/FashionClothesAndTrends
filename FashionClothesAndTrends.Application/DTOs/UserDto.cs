namespace FashionClothesAndTrends.Application.DTOs;

public class UserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    
    public string Token { get; set; }
}