
using Basket_Api.Basket.StoreBasket;

namespace Basket_Api.Basket.DeleteBasket
{
	// public record DeleteBasketRequest(string UserName);
	public record DeleteBasketResponse(bool IsSuccess);
	public class DeleteBasketEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
			{
				var result = await sender.Send(new DeleteBasketCommand(username));
				var response = result.Adapt<DeleteBasketResponse>();
				return Results.Ok(response);
			}).WithName("DeleteBasket")
			.WithDescription("Delete Basket")
			.Produces<StoreBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Delete Basket"); ;
		}
	}
}
