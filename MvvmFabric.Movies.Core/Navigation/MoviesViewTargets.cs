using System;

namespace MvvmFabric.Movies.Core.Navigation
{
	public class MoviesViewTargets : ViewTargets
	{
		protected internal MoviesViewTargets()
		{
		}

		protected MoviesViewTargets(int value)
			: base(value)
		{
		}

		public static ViewTargets Detail = 2;
		public static ViewTargets AdvancedSearch = 3;
	}
}
