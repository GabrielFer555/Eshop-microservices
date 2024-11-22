

namespace Basket_Api.Data
{
	public class BasketRepositoryi(IDocumentSession session) : IBasketRepository
	{
		public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
		{
			var basket = await session.LoadAsync<ShoppingCart>(username, cancellationToken);
			if(basket is null)
			{
				throw new NotFoundException("Basket", username);
			}
			return basket;
		}

		public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
		{
			session.Store(cart);
			await session.SaveChangesAsync(cancellationToken);

			return cart;
		}
		public Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
