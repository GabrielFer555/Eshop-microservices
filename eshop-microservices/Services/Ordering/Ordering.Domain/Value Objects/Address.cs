
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
    }
}
