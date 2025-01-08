
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

		public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment)
		{
			var order = new Order()
			{
				Id = id,
				CustomerId = customerId,
				OrderName = orderName,
				BillingAddress = billingAddress,
				ShippingAddress = shippingAddress,
				Payment = payment,
				OrderStatus = OrderStatus.Pending
			};

			order.AddDomainEvents(new OrderCreatedEvent(order));
			return order;
		}
		public void Update( OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment, OrderStatus orderStatus)
		{
			OrderName = orderName;
			BillingAddress = billingAddress;
			ShippingAddress = shippingAddress;
			Payment = payment;
			OrderStatus = orderStatus;



			AddDomainEvents(new OrderUpdatedEvent(this));
		}

		public void Add(ProductId productId, decimal price, int quantity)
		{
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

			var orderItem = new OrderItem(Id, productId, price, quantity);

			_orderItems.Add(orderItem);
		}

		public void Remove(ProductId productId) {
			var orderItemToRemove = _orderItems.FirstOrDefault(x => x.ProductId == productId);
			if (orderItemToRemove is not null) { 
				_orderItems.Remove(orderItemToRemove);
			}
		}


	}
}
