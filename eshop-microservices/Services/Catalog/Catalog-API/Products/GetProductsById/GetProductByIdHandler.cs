using Catalog_API.Products.GetProducts;
using Marten.Linq.QueryHandlers;

namespace Catalog_API.Products.GetProductsById
{
	public record IGetProductByIdQuery(Guid Id):IQuery<IGetProductByIdResult>;
	public record IGetProductByIdResult(Product? Product );
	public class GetProductByIdQueryHandler(IDocumentSession session, ILogger logger) : IQueryHandler<IGetProductByIdQuery, IGetProductByIdResult>
	{
		public async Task<IGetProductByIdResult> Handle(IGetProductByIdQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation($"GetProductsByIdQuery consulted with the following query {query}");
			var products = await session.Query<Product>().FirstOrDefaultAsync<Product>(prod => prod.Id == query.Id);
			return new IGetProductByIdResult(products);
		}
	}
}
