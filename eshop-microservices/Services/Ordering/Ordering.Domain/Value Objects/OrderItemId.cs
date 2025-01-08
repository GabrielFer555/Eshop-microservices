using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Value_Objects
{
	public record OrderItemId
	{
        public Guid Value { get; set; }

		private OrderItemId(Guid value) => Value = value;

		public static OrderItemId Of(Guid value)
		{
			ArgumentNullException.ThrowIfNull(value);
			if(value == Guid.Empty)
			{
				throw new ArgumentException("OrderItemId must be informed");
			}

			OrderItemId id = new OrderItemId(value);
			return id;
		}
    }
}
