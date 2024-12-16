

namespace Basket_Api.Basket.GetBasket
{
	// public record GetBasketRequest()
	public record GetBasketResponse(ShoppingCart ShoppingCart);
	public class GetBasketEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/basket/{username}",async (string username, ISender sender) =>
			{
				var result = await sender.Send(new GetBasketQuery(username));
				var response = result.Adapt<GetBasketResponse>();
				return Results.Ok(response);
			}).WithName("GetBasketById")
			.WithDescription("Find Basket based of its Id")
			.Produces<GetBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Get Basket by Id");
		}
	}
}
