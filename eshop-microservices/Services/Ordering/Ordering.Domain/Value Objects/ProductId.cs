using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Value_Objects
{
	public record ProductId
	{
        public Guid Value { get; set; }

		private ProductId(Guid value) => Value = value;

		public static ProductId Of(Guid value) {
			ArgumentNullException.ThrowIfNull(value);
			
			return new ProductId(value);
		}
    }
}
