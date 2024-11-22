


namespace Basket_Api.Basket.GetBasket
{
	public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;
	public record GetBasketResult(ShoppingCart ShoppingCart);
	public class GetBasketEndpoint : IQueryHandler<GetBasketQuery, GetBasketResult>
	{
		public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
		{
			/*
			var shoppingCart = await session.LoadAsync<ShoppingCart>(query.Username, cancellationToken);
			if(shoppingCart is null)
			{
				throw new Exception();
			}*/
			return new GetBasketResult(new ShoppingCart("Daniel"));
		}
	}
}
