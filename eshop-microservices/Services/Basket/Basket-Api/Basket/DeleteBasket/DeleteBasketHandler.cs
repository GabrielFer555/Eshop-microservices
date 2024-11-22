
namespace Basket_Api.Basket.DeleteBasket
{
	public record DeleteBasketCommand(string Username):ICommand<DeleteBasketResult>;
	public record DeleteBasketResult(bool IsSuccess);
	public class DeleteClassCommandValidator : AbstractValidator<DeleteBasketCommand>
	{
		public DeleteClassCommandValidator()
		{
			RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
		}
	}
	internal class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
	{
		public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
		{
			//database


			return new DeleteBasketResult(true);
		}
	}
}
