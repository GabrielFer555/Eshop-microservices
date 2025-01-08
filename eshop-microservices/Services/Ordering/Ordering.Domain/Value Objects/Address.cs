
namespace Ordering.Domain.Value_Objects
{
    public record Address // records types are better for value objects
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string EmailAddress { get; init; } = default!;
        public string AddressLine { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string State { get; init; } = default!;
        public string City { get; init; } = default!;
        public string ZipCode { get; init; } = default!;

        private Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string city, string zipCode ) { 
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            City = city;
            ZipCode = zipCode;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string city, string zipCode)
        {
            ArgumentException.ThrowIfNullOrEmpty(addressLine);
            ArgumentException.ThrowIfNullOrEmpty(country);

            return new Address(firstName, lastName, emailAddress, addressLine, country, state, city, zipCode);
        }
    }
}
