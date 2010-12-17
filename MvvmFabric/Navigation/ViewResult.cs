using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmFabric.Navigation
{
	/// <summary>
	/// ViewResult is the result of the dynamic build process.
	/// </summary>
	public class ViewResult
	{
		/// <summary>
		/// Gets the view.
		/// </summary>
		public FrameworkElement View { get; private set; }

		/// <summary>
		/// Gets the <see cref="ViewTargets"/> value indicating which view this is.
		/// </summary>
		public ViewTargets ViewTarget { get; private set; }
		
		/// <summary>
		/// Constructor for ViewResult.
		/// </summary>
		/// <param name="view">The view.</param>
		/// <param name="viewTarget">
		/// The <see cref="ViewTargets"/> value indicating which view this is.
		/// </param>
		public ViewResult(FrameworkElement view, ViewTargets viewTarget)
		{
			View = view;
			ViewTarget = viewTarget;
		}
	}
}
