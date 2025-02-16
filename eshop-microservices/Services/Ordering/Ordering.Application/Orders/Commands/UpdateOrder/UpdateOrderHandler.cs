
namespace Ordering.Application.Orders.Commands.UpdateOrder
{
	public class UpdateOrderHandler(IApplicationDbContext dbContext)
		: ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
	{
		public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
		{

			var orderId = OrderId.Of(command.Order.Id);
			var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
			if(order is null)
			{
				throw new OrderNotFoundException(command.Order.Id);
			}
			UpdateOrderWithNewValues(order, command.Order);
			await dbContext.SaveChangesAsync(cancellationToken);


			return new UpdateOrderResult(true);

		}

		public void UpdateOrderWithNewValues(Order order, OrderDto dto) {
			var shippingAddress = Address.Of(dto.ShippingAddress.FirstName, dto.ShippingAddress.LastName,
				dto.ShippingAddress.EmailAddress,
				dto.ShippingAddress.AddressLine, dto.ShippingAddress.Country,
				dto.ShippingAddress.State, dto.ShippingAddress.ZipCode);

			var billingAddress = Address.Of(dto.BillingAddress.FirstName, dto.BillingAddress.LastName,
				dto.BillingAddress.EmailAddress,
				dto.BillingAddress.AddressLine, dto.BillingAddress.Country,
				dto.BillingAddress.State, dto.BillingAddress.ZipCode);

			var payment = Payment.Of(dto.Payment.CardName, dto.Payment.CardNumber,
				dto.Payment.Expiration, dto.Payment.Cvv, dto.Payment.PaymentMethod
				);

			order.Update(
				OrderName.Of(dto.OrderName),
				billingAddress: billingAddress,
				shippingAddress: shippingAddress,
				payment: payment,
				orderStatus: dto.Status
				);

		}
	}
}
