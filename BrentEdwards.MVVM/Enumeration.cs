using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrentEdwards.MVVM
{
	public abstract class Enumeration<TValue>
		where TValue : struct
	{
		protected TValue Value { get; set; }

		protected Enumeration()
		{
			Value = default(TValue);
		}

		protected Enumeration(TValue value)
		{
			Value = value;
		}

		public static implicit operator TValue(Enumeration<TValue> obj)
		{
			return obj.Value;
		}
	}
}
