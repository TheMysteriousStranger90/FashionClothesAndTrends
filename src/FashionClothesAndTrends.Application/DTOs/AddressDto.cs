using System.ComponentModel.DataAnnotations;

namespace FashionClothesAndTrends.Application.DTOs;

public class AddressDto
{
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Country { get; set; } = string.Empty;
    [Required] public string City { get; set; } = string.Empty;
    [Required] public string State { get; set; } = string.Empty;
    [Required] public string AddressLine { get; set; } = string.Empty;
    [Required] public string PostalCode { get; set; } = string.Empty;
}
