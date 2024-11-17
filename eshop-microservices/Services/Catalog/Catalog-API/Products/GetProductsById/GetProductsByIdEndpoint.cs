
using Catalog_API.Products.GetProducts;

namespace Catalog_API.Products.GetProductsById
{
	public record GetProductByIdResponse(Product? Product);
	public class GetProductsByIdEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
			{
				var send = await sender.Send(new GetProductByIdQuery(id));

				var result = send.Adapt<GetProductByIdResponse>();
				
				return Results.Ok(result);
				
			}).WithName("GetProductbyId")
			.WithDescription("Find Product based of its Id")
			.Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Get Product by Id");
		}
	}
}
