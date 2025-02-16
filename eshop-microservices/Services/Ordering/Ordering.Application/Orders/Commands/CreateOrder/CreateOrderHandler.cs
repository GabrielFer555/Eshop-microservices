namespace Ordering.Application.Orders.Commands.CreateOrder
{
	public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
	{
		public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
		{


			//Create Order entity from common object
			//save to database
			//return result
			var order = CreateNewOrder(command);
			dbContext.Orders.Add(order);
			await dbContext.SaveChangesAsync();

			return new CreateOrderResult(order.Id.Value);
		}
		private Order CreateNewOrder(CreateOrderCommand order) { 
			var shippingAddress = Address.Of(order.Order.ShippingAddress.FirstName, order.Order.ShippingAddress.LastName,
				order.Order.ShippingAddress.EmailAddress, 
				order.Order.ShippingAddress.AddressLine, order.Order.ShippingAddress.Country, 
				order.Order.ShippingAddress.State, order.Order.ShippingAddress.ZipCode);

			var billingAddress = Address.Of(order.Order.BillingAddress.FirstName, order.Order.BillingAddress.LastName,
				order.Order.BillingAddress.EmailAddress,
				order.Order.BillingAddress.AddressLine, order.Order.BillingAddress.Country,
				order.Order.BillingAddress.State, order.Order.BillingAddress.ZipCode);

			var payment = Payment.Of(order.Order.Payment.CardName, order.Order.Payment.CardNumber,
				order.Order.Payment.Expiration, order.Order.Payment.Cvv, order.Order.Payment.PaymentMethod
				);

			var orderCreated = Order.Create(
				id: OrderId.Of(Guid.NewGuid()),
				customerId: CustomerId.Of(order.Order.CustomerId),
				orderName: OrderName.Of(order.Order.OrderName),
				billingAddress: billingAddress,
				shippingAddress: shippingAddress,
				payment: payment);

			foreach(var orderItem in order.Order.OrderItems)
			{
				orderCreated.Add(ProductId.Of(orderItem.ProductId), orderItem.Price, orderItem.Quantity);
			}

			return orderCreated;


		}
	}
}
