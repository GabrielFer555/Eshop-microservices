
namespace Ordering.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
	{
		public Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
