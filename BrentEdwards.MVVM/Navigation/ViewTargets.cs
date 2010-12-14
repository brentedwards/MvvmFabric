using System;

namespace BrentEdwards.MVVM
{
	public class ViewTargets : Enumeration<int>
	{
		protected internal ViewTargets()
		{
		}

		protected ViewTargets(int value)
			: base(value)
		{
		}

		public static implicit operator ViewTargets(int value)
		{
			return new ViewTargets(value);
		}

		public static ViewTargets DefaultView = 1;
	}
}
