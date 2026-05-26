using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class ShippingAddress
{
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public bool? IsDefault { get; set; } = false;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}
