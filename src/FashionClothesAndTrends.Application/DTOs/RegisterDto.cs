using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Application.DTOs
{
    public class RegisterDto
    {
        [Required] public string FirstName { get; set; } = string.Empty;

        [Required] public string LastName { get; set; } = string.Empty;

        [Required] public string Gender { get; set; } = string.Empty;

        [Required] public DateOnly DateOfBirth { get; set; }

        [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/<>.,])(?!.*\\s).*$",
            ErrorMessage =
                "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; } = string.Empty;
    }
}

