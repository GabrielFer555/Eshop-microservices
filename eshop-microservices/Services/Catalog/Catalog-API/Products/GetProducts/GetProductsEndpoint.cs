


namespace Catalog_API.Products.GetProducts
{
	public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
	public record GetProductResponse(IEnumerable<Product> Products, long PageNumber, long PageSize, long TotalPages);
	public class GetProductsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
			{
				var query = request.Adapt<GetProductsQuery>();
				var result = await sender.Send(query);
				var response = result.Adapt<GetProductResponse>();
				return Results.Ok(response);
			}).WithName("GetProducts").
			Produces<GetProductResponse>(StatusCodes.Status200OK).
			ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Get Producst")
			.WithDescription("Get Products"); ; 
		}
	}
}
