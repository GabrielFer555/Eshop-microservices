﻿

using Catalog_API.Products.CreateProduct;
using Microsoft.AspNetCore.Builder;

namespace Catalog_API.Products.GetProducts
{
	public record GetProductQueryResult(IEnumerable<Product> Products);
	public class GetProductsEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products", async (ISender sender) =>
			{
				var result = await sender.Send(new GetProductsQuery());
				var response = result.Adapt<GetProductQueryResult>();
				return Results.Ok(response);
			}).WithName("GetProducts").
			Produces<CreateProductResponse>(StatusCodes.Status200OK).
			ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Get Producst")
			.WithDescription("Get Products"); ; 
		}
	}
}