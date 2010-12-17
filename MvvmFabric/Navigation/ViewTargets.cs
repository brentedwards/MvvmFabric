using System;

namespace MvvmFabric
{
	/// <summary>
	/// The ViewTargets is a pseudo enumeration used by the navagation.
	/// </summary>
	public class ViewTargets : Enumeration<int>
	{
		/// <summary>
		/// The default view of the application.
		/// </summary>
		public static ViewTargets DefaultView = 1;

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
	}
}
