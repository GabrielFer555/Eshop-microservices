﻿
using System.Linq;
using Marten.Pagination;

namespace Catalog_API.Products.GetProductByCategory
{
	public record GetProductByCategoryQuery(string Category, int? PageNumber=1, int? PageSize=10):IQuery<GetProductByCategoryResult>;

	public record GetProductByCategoryResult(IEnumerable<Product> Products, long PageNumber, long PageSize, long TotalPages);
	internal class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{

			var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Category)).ToPagedListAsync(query.PageNumber?? 1, query.PageSize?? 10, cancellationToken);
			
			return new GetProductByCategoryResult(products, products.PageNumber, products.PageSize, products.PageCount);

		}
	}
}
