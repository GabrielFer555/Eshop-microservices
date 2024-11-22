
namespace Basket_Api.Basket.StoreBasket
{
	public record StoreBasketRequest(ShoppingCart Cart);
	public record StoreBasketResponse(string UserName);
	public class StoreBasketEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/basket", async(ISender sender, StoreBasketRequest request) => {
				var command = request.Adapt<StoreBasketCommand>();
				var result = await sender.Send(command);
				var response = result.Adapt<StoreBasketResponse>();
				return Results.Created($"/basket/{response.UserName}",response);

			}).WithName("CreateOrUpdateBasket")
			.WithDescription("Create or Update Basket based of its Id")
			.Produces<StoreBasketResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("CreateOrUpdateBasket");

		}
	}
}
