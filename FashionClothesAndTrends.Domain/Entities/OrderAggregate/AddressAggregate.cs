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

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }

    public string City { get; set; }
    public string State { get; set; }
    public string AddressLine { get; set; }
    public string PostalCode { get; set; }
}