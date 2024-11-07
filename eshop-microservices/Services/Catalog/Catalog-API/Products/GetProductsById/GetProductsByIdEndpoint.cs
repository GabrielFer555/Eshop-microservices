
using Catalog_API.Products.GetProducts;

namespace Catalog_API.Products.GetProductsById
{
	public record IGetProductByIdQueryResult(Product? Product);
	public class GetProductsByIdEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/products/{id}", async (Guid id, ISender sender) =>
			{
				var send = await sender.Send(new IGetProductByIdQuery(id));

				var result = send.Adapt<IGetProductByIdQueryResult>();
				if(result.Product is null)
				{
					return Results.NotFound();
				}
				else
				{
					return Results.Ok(result);
				}
			});
		}
	}
}
