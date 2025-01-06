
namespace Ordering.Domain.Models
{
    // This is an aggregate
    public class Order : Aggregate<OrderId>
	{
		private readonly List<OrderItem> _orderItems = new();
		public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly(); // 1..N relation
		public CustomerId CustomerId { get; set; } = default!;
		public OrderName OrderName { get; private set; } = default!;

		public Address BillingAddress { get; private set; } = default!;

		public Address ShippingAddress { get; private set; } = default!;

		public Payment Payment { get; private set; } = default!;

		public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending!;

		public decimal TotalPrice
		{
			get => _orderItems.Sum(x => x.Price * x.Quantity);
			private set {}
		}


	}
}
