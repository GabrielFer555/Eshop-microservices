

namespace Catalog_API.Products.GetProductByCategory
{
	public record GetProductByCategoryRequest(int? PageNumber=1, int? PageSize=10);
	
	public record GetProductByCategoryResponse(IEnumerable<Product> Products, long PageNumber, long PageSize, long TotalPages);
	public class GetProductByCategoryEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/category/{category}", async (string category, [AsParameters] GetProductByCategoryRequest queryParams, ISender sender) =>
			{
				var result = await sender.Send(new GetProductByCategoryQuery(category, queryParams.PageNumber, queryParams.PageSize));

				var response = result.Adapt<GetProductByCategoryResponse>();
				return Results.Ok(response);
			}).WithName("GetProductByCategory")
			.Produces(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithDescription("Get Product by Category")
			.WithDisplayName("GetProductByCategory");
		}
	}
}
