using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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

		public override string ToString()
		{
			var fields = GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach (var field in fields)
			{
				var obj = field.GetValue(this);
				var value = ((Enumeration<TValue>)obj).Value;

				if (value.Equals(Value))
				{
					return field.Name;
				}
			}

			return string.Empty;
		}
	}
}
