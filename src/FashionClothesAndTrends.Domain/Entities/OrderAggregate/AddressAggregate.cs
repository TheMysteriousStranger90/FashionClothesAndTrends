namespace FashionClothesAndTrends.Domain.Entities.OrderAggregate;

public class AddressAggregate
{
    public AddressAggregate()
    {
    }

    public AddressAggregate(string firstName, string lastName, string street, string city, string state,
        string postalcode, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        Country = country;
        AddressLine = street;
        City = city;
        State = state;
        PostalCode = postalcode;
    }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}