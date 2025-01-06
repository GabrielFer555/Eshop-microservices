﻿
namespace Ordering.Domain.Models
{
	public class OrderItem:Entity<OrderItemId>
	{

        public decimal Price { get; private set; } = default!;
        public OrderId OrderId { get; private set; } = default!;
		public ProductId ProductId { get; private set; } = default!;
		public int Quantity { get; private set; } = default!;

		internal OrderItem(decimal price, Guid orderId, Guid productId, int quantity) // internal constructor
		{
			Id = OrderItemId.Of(Guid.NewGuid());
			Price = price;
			OrderId = orderId;
			ProductId = productId;
			Quantity = quantity;
		}
	}
}
