using System;

namespace BrentEdwards.MVVM.Movies.Core.Navigation
{
	public sealed class MoviesViewTargets : ViewTargets
	{
		protected internal MoviesViewTargets()
		{
		}

		protected MoviesViewTargets(int value)
			: base(value)
		{
		}

		public static ViewTargets Detail = 2;
	}
}
