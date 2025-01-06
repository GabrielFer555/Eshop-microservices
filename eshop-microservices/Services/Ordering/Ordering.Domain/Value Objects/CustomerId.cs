namespace Ordering.Domain.Value_Objects
{
    public record CustomerId 
    {
        public Guid Value { get; }
        private CustomerId(Guid id) => Value = id;
        public static CustomerId Of(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            if(id == Guid.Empty)
            {
                throw new DomainException("CustomerId must not be null");
            }
            return new CustomerId(id);
        }
    }
}
