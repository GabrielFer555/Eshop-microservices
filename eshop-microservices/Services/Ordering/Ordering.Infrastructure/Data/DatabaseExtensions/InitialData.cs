namespace Ordering.Infrastructure.Data.DatabaseExtensions
{
	public static class InitialData
	{
		public static List<Customer> Customers { get; } = new()
		{
			Customer.Create(CustomerId.Of(new Guid("550e8400-e29b-41d4-a716-446655440000")), "Alice Johnson", "alice.johnson@example.com"),
			Customer.Create(CustomerId.Of(new Guid("e8b4a123-1af4-4bfc-9c92-9d50a00a8e4d")), "Bob Smith", "bob.smith@example.com"),
			Customer.Create(CustomerId.Of(new Guid("1e4de662-f83a-41a5-90a4-3d784b9ccf77")), "Charlie Brown", "charlie.brown@example.com"),
			Customer.Create(CustomerId.Of(new Guid("8a4df9c2-9085-4b87-8b5a-d3c01f22d4b1")), "Diana Prince", "diana.prince@example.com"),
			Customer.Create(CustomerId.Of(new Guid("6e1f6725-d9f3-4a36-994a-80c252d6a012")), "Ethan Hunt", "ethan.hunt@example.com")
		};

		public static List<Product> Products { get; } = new()
		{
			Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "Laptop", 1299.99m),
			Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Smartphone", 799.99m),
			Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Headphones", 199.99m),
			Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Smartwatch", 249.99m)
		};

		public static List<Order> OrdersWithItems
		{
			get
			{
				var address1 = Address.Of("Mehmet", "Ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turkey", "Istanbul", "38050");
				var address2 = Address.Of("John", "Doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

				var payment1 = Payment.Of("Mehmet", "5555555555554444", "12/28", "355", 1);
				var payment2 = Payment.Of("John", "8885555555554444", "06/30", "222", 2);

				var order1 = Order.Create(
					OrderId.Of(Guid.NewGuid()),
					CustomerId.Of(new Guid("550e8400-e29b-41d4-a716-446655440000")), // Alice Johnson
					OrderName.Of("ORD_1"),
					address1,
					address1,
					payment1);
				order1.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 1299,4 ); // Laptop
				order1.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 799, 2); // Smartphone

				var order2 = Order.Create(
					OrderId.Of(Guid.NewGuid()),
					CustomerId.Of(new Guid("e8b4a123-1af4-4bfc-9c92-9d50a00a8e4d")), // Bob Smith
					OrderName.Of("ORD_2"),
					address2,
					address2,
					payment2);
				order2.Add(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 199, 2); // Headphones
				order2.Add(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 249, 1); // Smartwatch

				return new List<Order> { order1, order2 };
			}
		}
	}
}
