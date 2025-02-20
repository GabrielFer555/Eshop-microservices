namespace Ordering.Domain.Value_Objects
{
    public record OrderId
	{
		public Guid Value { get; }
		private OrderId(Guid id) => Value = id;
		public static OrderId Of(Guid id)
		{
			ArgumentNullException.ThrowIfNull(id);
			if (id == Guid.Empty)
			{
				throw new DomainException("OrderId must not be null");
			}
			return new OrderId(id);
		}
	}
}
