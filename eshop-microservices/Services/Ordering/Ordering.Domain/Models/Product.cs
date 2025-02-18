﻿using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
	public class Product:Entity<ProductId>
	{
		public string Name { get; private set; } = default!;
		public decimal Price { get; set; } = default!;


		public static Product Create(ProductId id ,string name, decimal price) { 
			ArgumentException.ThrowIfNullOrWhiteSpace(name);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

			return new Product
			{
				Name = name,
				Price = price,
				Id = id
			};
		}
    }
}
