namespace Ordering.Domain.Value_Objects
{
	public record OrderName
	{
		private const int DefaultLenght = 5;
		public string Value { get; } = default!;

		private OrderName(string value) => Value = value;
		
		public static OrderName Of(string value)
		{
			ArgumentException.ThrowIfNullOrEmpty(value);
			ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLenght);

			return new OrderName(value);
		} 

	}
}
