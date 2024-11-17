
using Catalog_API.Products.CreateProduct;

namespace Catalog_API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(
		bool IsSuccess);
	public class UpdateProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
			{
				
					var body = request.Adapt<UpdateProductCommand>();
					var result = await sender.Send(body);
					var response = result.Adapt<UpdateProductResponse>();
					return Results.Ok(response);
				
				
				
;			}).WithName("UpdateProduct").
			Produces<CreateProductResponse>(StatusCodes.Status204NoContent).
			ProducesProblem(StatusCodes.Status400BadRequest).
			ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Update Product")

			.WithDescription("Update Product"); ;
		}
	}
}
