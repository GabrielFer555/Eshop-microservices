

using Basket_Api.Basket.StoreBasket;

namespace Basket_Api.Basket.CheckoutBasket
{
    public record BasketCheckoutRequest(BasketCheckoutDto BasketCheckout);
    public record BasketCheckoutResponse(bool IsSuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (BasketCheckoutRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<BasketCheckoutResponse>();

                return Results.Ok(response);
            }).WithName("BasketCheckout")
            .WithDescription("Basket Checkout")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Basket Checkout"); ; ;
        }
    }
}
