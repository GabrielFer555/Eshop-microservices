using Catalog_API.Products.GetProducts;
using Marten.Linq.QueryHandlers;

namespace Catalog_API.Products.GetProductsById
{
	public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
	public record GetProductByIdResult(Product? Product );
	public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
	{
		public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
		{
			var products = await session.LoadAsync<Product>(query.Id, cancellationToken);
			if(products is null)
			{
				throw new ProductNotFoundException(query.Id);
			}

			return new GetProductByIdResult(products);
		}
	}
}
