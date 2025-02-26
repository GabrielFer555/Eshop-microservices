
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket_Api.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout):ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    
    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckout).NotEmpty().WithMessage("BasketCheckoutDto must be informed");   
            RuleFor(x => x.BasketCheckout.UserName).NotNull().WithMessage("Username is required");   
        }
    }
    public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.BasketCheckout.UserName, cancellationToken);
            if (basket == null) return new CheckoutBasketResult(false);
            var checkoutBasket = command.BasketCheckout.Adapt<BasketCheckoutEvent>();
            checkoutBasket.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(checkoutBasket);
            await repository.DeleteBasket(basket.Username, cancellationToken);

            return new CheckoutBasketResult(true);


        }
    }
}
