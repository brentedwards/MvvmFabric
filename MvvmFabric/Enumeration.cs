using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MvvmFabric
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
			var blah = GetType();
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

			return Value.ToString();
		}

		public override bool Equals(object obj)
		{
			var equal = false;
			if (obj is Enumeration<TValue>)
			{
				var enumeration = (Enumeration<TValue>)obj;
				equal = Value.Equals(enumeration.Value);
			}
			return equal;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}
