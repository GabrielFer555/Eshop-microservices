
using Discout.Grpc;

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
	public class StoreBasketHandler(IBasketRepository _repository, DiscountProtoService.DiscountProtoServiceClient discountProtoService) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			await DeductDiscount(command.Cart, cancellationToken);
			var insertedCart = await _repository.StoreBasket(command.Cart, cancellationToken);

			return new StoreBasketResult(insertedCart.Username);
		}

		public async Task DeductDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
		{
			foreach (var item in shoppingCart.Items)
			{
				var discount = await discountProtoService.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName } );
				item.Price -= discount.Amount;
			}
			
		}
	}
}
