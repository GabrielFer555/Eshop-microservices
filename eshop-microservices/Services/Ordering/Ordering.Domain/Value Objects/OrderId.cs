using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Value_Objects
{
	public record OrderId
	{
		public Guid Value { get; }
		private OrderId(Guid id) => Value = id;
		public static OrderId Of(Guid id)
		{
			ArgumentNullException.ThrowIfNull(id);
			if (id == Guid.Empty)
			{
				throw new DomainException("CustomerId must not be null");
			}
			return new OrderId(id);
		}
	}
}
