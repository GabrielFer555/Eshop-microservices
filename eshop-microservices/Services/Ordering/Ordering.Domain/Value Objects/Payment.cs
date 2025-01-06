using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Value_Objects
{
	public class Payment
	{
		public string CardName { get; init; } = default!;
		public string CardNumber { get; init; } = default!;
		public string Expiration { get; init; } = default!;
		public string CVV { get; init; } = default!;
		public int PaymentMethod { get; init; } = default!;
	}
}
