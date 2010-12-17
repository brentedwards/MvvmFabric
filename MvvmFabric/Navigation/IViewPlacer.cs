using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// Interface for a View Placer.  A View Placer takes a view and places it into the
	/// user interface.
	/// </summary>
	public interface IViewPlacer
	{
		/// <summary>
		/// Places a view into the user interface.
		/// </summary>
		/// <param name="viewResult">
		/// The <see cref="ViewResult"/> containing the view to be placed.
		/// </param>
		void PlaceView(ViewResult viewResult);
	}
}
