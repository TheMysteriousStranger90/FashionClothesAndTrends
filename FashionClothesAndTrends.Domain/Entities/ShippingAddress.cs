using FashionClothesAndTrends.Domain.Common;

namespace FashionClothesAndTrends.Domain.Entities;

public class ShippingAddress
{
    public string AddressLine { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public bool? IsDefault { get; set; } = false;

    public string UserId { get; set; }
    public User User { get; set; }
}