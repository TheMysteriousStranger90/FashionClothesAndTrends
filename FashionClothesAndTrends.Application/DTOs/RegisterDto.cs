using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Application.DTOs
{
    public class RegisterDto
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string Gender { get; set; }

        [Required] public DateOnly DateOfBirth { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{\":;'?/<>.,])(?!.*\\s).*$",
            ErrorMessage =
                "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
    }
}