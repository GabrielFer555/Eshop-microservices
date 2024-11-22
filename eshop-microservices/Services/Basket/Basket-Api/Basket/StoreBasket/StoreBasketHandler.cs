
namespace Basket_Api.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
	public record StoreBasketResult(string UserName);
	public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
	{
		public StoreBasketCommandValidator()
		{
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
			RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is Required").When(x => x.Cart != null); ;
		}
	};
	public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			var cart = new ShoppingCart
			{
				Items = command.Cart.Items,
				Username=command.Cart.Username,
			};

			//TODO INSERT OR UPDATE REGISTER USING MARTEN LIBRARY
			//UPDATE CACHE ON REDIS

			return new StoreBasketResult(command.Cart.Username);
		}
	}
}
